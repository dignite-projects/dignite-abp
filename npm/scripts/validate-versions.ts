import { program } from "commander";    // 命令行参数解析库
import fse from "fs-extra";             // 增强版文件系统模块
import * as path from "path";           // 路径处理模块
import { log } from "./utils/log";      // 自定义日志模块

let excludedPackages = []; // 存储排除包名的数组

// 立即执行异步函数
(async () => {
  initCommander();  // 初始化命令行参数
  await compare();  // 执行核心比较逻辑
})();

function initCommander() {
  program.requiredOption(
    "-v, --compareVersion <version>",
    "version to compare"
  );
  // 必须参数：扫描路径
  program.requiredOption("-p, --path <path>", "NPM packages folder path");
   // 可选参数：排除检查的包
  program.option(
    "-ep, --excludedPackages <excludedpackages>",
    "Packages that will not be checked. Can be passed with separeted comma (like @dignite-ng/utils,@dignite-ng/core)",
    ""
  );
  program.parse(process.argv);

  excludedPackages = program.opts().excludedPackages.split(",");
}



async function compare() {
  // 获取并处理路径
  let { compareVersion, path: packagesPath } = program.opts();
  packagesPath = path.resolve(packagesPath); // 转换为绝对路径

  // 读取目标目录下的所有包
  const packageFolders = await fse.readdir(packagesPath);

  // 遍历每个包
  for (let i = 0; i < packageFolders.length; i++) {
    const folder = packageFolders[i];
    const pkgJsonPath = `${packagesPath}/${folder}/package.json`;
    
    // 读取package.json
    let pkgJson;
    try {
      pkgJson = await fse.readJSON(pkgJsonPath);
    } catch (error) {} // 静默处理读取错误

    // 版本检查
    if (!excludedPackages.includes(pkgJson.name) && 
        pkgJson.version !== compareVersion) {
      throwError(pkgJsonPath, pkgJson.name, pkgJson.version);
    }

    // 依赖项检查
    const { dependencies } = pkgJson;
    if (dependencies) {
      await compareDependencies(dependencies, pkgJsonPath);
    }
  }
}

async function compareDependencies(
  dependencies: Record<string, string>,
  filePath: string
) {
  const entries = Object.entries(dependencies);
  
  for (const [packageName, version] of entries) {
    // 清理版本号（移除^/~前缀）
    const cleanVersion = getCleanVersionName(version);
    const targetVersion = getCleanVersionName(program.opts().compareVersion);

    // 检查条件：
    // 1. 不在排除列表
    // 2. 是@dignite-ng或@volo作用域包
    // 3. 版本不匹配
    if (!excludedPackages.includes(packageName) &&
        /@(dignite-ng|volo)/.test(packageName) &&
        cleanVersion !== targetVersion) {
      throwError(filePath, packageName, targetVersion);
    }
  }
}

function throwError(filePath: string, pkg: string, version?: string) {
  const { compareVersion } = program.opts();

  log.error(
    `${filePath}: ${pkg} version is not ${compareVersion}. it is ${version}`
  );
  process.exit(1);
}

function getCleanVersionName(version) {
  // Remove caret (^) or tilde (~) from the beginning of the version number
  return version.replace(/^[\^~]+/, "");
}
