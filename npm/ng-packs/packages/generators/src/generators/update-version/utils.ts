// 从NX开发工具包导入所需方法
import { getProjects, readJson, readProjectConfiguration, Tree } from '@nx/devkit';

// 定义需要忽略的项目名称常量数组
export const IGNORED_PROJECT_NAMES = ['apex-chart-components', 'bs-components', 'workspace-plugin','schematics','generators'];

// 获取package.json文件路径列表的函数
export function getPackageJsonList(tree: Tree, packages: string[]): string[] {
  const project = getProjects(tree); // 获取工作区所有项目

  const result = ['/package.json']; // 初始化结果数组（包含根package.json）
  project.forEach((value, key) => {
    if (value.projectType !== 'library') {
      // 过滤非库项目
      return;
    }
    if (IGNORED_PROJECT_NAMES.some(x => x === key)) {
      // 过滤需要忽略的项目
      return;
    }
    const projectConfiguration = readProjectConfiguration(tree, key); // 读取项目配置

    if (packages.length && !packages.includes(value.name)) {
      // 按指定包名过滤
      return;
    }
    result.push(projectConfiguration.root + '/package.json'); // 添加库的package.json路径
  });
  return result;
}

// 获取包名称列表的函数
export function getPackageNameList(tree: Tree, packageJsonList: string[]) {
  return packageJsonList.map(packageJson => {
    const jsonFile = readJson(tree, packageJson); // 读取package.json文件
    return jsonFile.name; // 提取包名称
  });
}

// 定义Lepton主题相关包列表（当前注释状态）
const leptonPackages = [
  // "@dignite-ng/expand.schematics",
  // '@abp/ng.theme.lepton-x',
  // '@volosoft/ngx-lepton-x',
  // '@volo/abp.ng.lepton-x.core',
  // '@volo/ngx-lepton-x.core',
  // '@volo/ngx-lepton-x.lite',
  // '@volosoft/abp.ng.theme.lepton-x',
];

// 匹配@dignite-ng作用域下所有包的正则
const abpPackageNameRegex = /^@(dignite-ng)\/.*/;

// ABP包判断函数
export function isAbpPack(packageName) {
  return abpPackageNameRegex.test(packageName) && !leptonPackages.includes(packageName);
}

// Lepton主题包判断函数（当前逻辑未生效，因leptonPackages数组为空）
export function functionisLeptonXPack(packageName) {
  return leptonPackages.includes(packageName);
}

// 版本号分配工厂函数
export function getVersionByPackageNameFactory(abpVersionName: string, leptonXVersionName: string) {
  return (packageName: string) => {
    if (isAbpPack(packageName)) {
      return abpVersionName; // 返回ABP版本号
    }
    if (functionisLeptonXPack(packageName)) {
      return leptonXVersionName; // 返回LeptonX版本号
    }
    return ''; // 其他情况返回空字符串
  };
}

// 语义化版本正则表达式（支持预发布版本和构建元数据）
export const semverRegex =
  /\d+\.\d+\.\d+(?:-[a-zA-Z0-9]+(?:\.[a-zA-Z0-9-]+)*)?(?:\+[a-zA-Z0-9]+(?:\.[a-zA-Z0-9-]+)*)?$/;
