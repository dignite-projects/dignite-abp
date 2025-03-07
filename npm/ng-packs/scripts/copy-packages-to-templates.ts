import execa from "execa";
import fse from "fs-extra";
import fs from "fs";
import program from "commander";

const defaultTemplates = ['app', 'app-nolayers', 'module'];
const defaultTemplatePath = '../../../templates';
const packageMap = {
  // account: 'ng.account',
  // 'account-core': 'ng.account.core',
  // components: 'ng.components',
  // core: 'ng.core',
  // 'feature-management': 'ng.feature-management',
  // identity: 'ng.identity',
  // 'permission-management': 'ng.permission-management',
  // 'setting-management': 'ng.setting-management',
  // 'tenant-management': 'ng.tenant-management',
  // 'theme-basic': 'ng.theme.basic',
  // 'theme-shared': 'ng.theme.shared',
  // oauth: 'ng.oauth',
  'core': 'expand.core',
  'tenant-domain-management': 'expand.tenant-domain-management',
  'regionalization-management': 'expand.regionalization-management',
  'file-explorer': 'expand.file-explorer',
  'ck-editor': 'expand.ck-editor',
  'dynamic-form': 'expand.dynamic-form',
  'cms': 'expand.cms',
  // schematics: 'expand.schematics',
};
program.option('-t, --templates  <templates>', 'template dirs', false);
program.option('-p, --template-path <templatePath>', 'root template path', false);
program.option(
  '-e, --use-existing-build <useExistingBuild>',
  "don't build packages if dist folder exists",
  false,
);
program.option('-i, --noInstall', 'skip package installation', false);
program.option('-a, --absolute-dir <absoluteDir>', 'Absolute angular directory', false);
program.parse(process.argv);
const templates = program.templates ? program.templates.split(',') : defaultTemplates;
const templateRootPath = program.templatePath ? program.templatePath : defaultTemplatePath;
(async () => {
  if (!program.useExistingBuild) {
    await execa('yarn', ['build:all'], {
      stdout: 'inherit',
      cwd: '../',
    });
  }

  if (!program.noInstall) {
    await installPackages();
  }

  await removeDigniteNgPackages();

  await copyBuildedPackagesFromDistFolder();
})();

async function runEachTemplate(
  handler: (template: string, templatePath?: string) => void | Promise<any>,
) {
  if (program.absoluteDir) {
    const result = handler('', program.absoluteDir);
    result instanceof Promise ? await result : result;
  } else {
    for (var template of templates) {
      const templatePath = `${templateRootPath}/${template}/angular`;
      const result = handler(template, templatePath);
      result instanceof Promise ? await result : result;
    }
  }

}

async function installPackages() {
  await runEachTemplate(async (template, templatePath) => {
    if (fse.existsSync(`${templatePath}/yarn.lock`)) {
      fse.removeSync(`${templatePath}/yarn.lock`);
    }
    await execa('yarn', ['install', '--ignore-scripts'], {
      stdout: 'inherit',
      cwd: templatePath,
    });
  });
}

async function removeDigniteNgPackages() {
  // 异步函数，用于遍历所有模板并移除指定DigniteNg包
  await runEachTemplate(async (template, templatePath) => {
    // 遍历packageMap的所有值（映射后的包名）
    Object.values(packageMap).forEach(value => {
      // 构造目标路径：模板目录/node_modules/@dignite-ng/包名
      const path = `${templatePath}/node_modules/@dignite-ng/${value}`;
      // 如果路径存在则同步删除
      if (fs.existsSync(path)) {
        fse.removeSync(path);  // 使用fs-extra的递归删除方法
      }
    });
    
    // 额外处理.angular目录（可能是Angular的缓存目录）
    if (fs.existsSync(`${templatePath}/.angular`)) {
      fse.removeSync(`${templatePath}/.angular`);
    }
  });
}
function createFolderIfNotExists(destination: string) {
  destination.split('/').reduce((acc, dir) => {
    if (!fs.existsSync(acc)) {
      fs.mkdirSync(acc);
    }
    return `${acc}/${dir}`;
  });
}
async function copyBuildedPackagesFromDistFolder() {
  // 异步函数，用于将构建好的包从dist目录复制到各个模板中
  await runEachTemplate(async (template, templatePath) => {
    // 遍历packageMap的键值对
    Object.entries(packageMap).forEach(([key, value]) => {
      // 确保目标目录存在，如果不存在则创建
      createFolderIfNotExists(`${templatePath}/node_modules/@dignite-ng/${value}`);
      // 将构建好的包从dist目录复制到node_modules
      fse.copySync(`../dist/packages/${key}/`, `${templatePath}/node_modules/@dignite-ng/${value}`);
    });
  });
}
