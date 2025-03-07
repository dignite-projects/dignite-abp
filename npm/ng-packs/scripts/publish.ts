// 导入commander库，用于处理命令行参数
import program from 'commander';

// 导入execa库，用于执行shell命令
import execa from 'execa';

// 导入fs-extra库，提供增强的文件系统操作
import fse from 'fs-extra';

// 导入本地replace-with-preview模块
import replaceWithPreview from './replace-with-preview';

// 导入semver库的parse函数，用于解析版本号
const semverParse = require('semver/functions/parse');

// 配置命令行选项
program
  .option(
    '-v, --nextVersion <version>',  // 定义下一个版本号参数
    'next semantic version. Available versions: ["major", "minor", "patch", "premajor", "preminor", "prepatch", "prerelease", "or type a custom version"]',
  )
  .option('-r, --registry <registry>', 'target npm server registry')  // 定义npm registry参数
  .option('-p, --preview', 'publishes with preview tag')  // 定义预览发布标志
  .option('-sg, --skipGit', 'skips git push')  // 定义跳过git push标志
  .option('-sv, --skipVersionValidation', 'skips version validation');  // 定义跳过版本验证标志

// 解析命令行参数
program.parse(process.argv);

// 定义异步立即执行函数
(async () => {
  // 定义支持的版本类型
  const versions = ['major', 'minor', 'patch', 'premajor', 'preminor', 'prepatch', 'prerelease'];

  // 读取当前版本号
  const oldVersion = fse.readJSONSync('../lerna.version.json').version;

  // 检查是否提供了nextVersion参数
  if (!program.nextVersion) {
    console.error('Please provide a version with --nextVersion attribute');
    process.exit(1);
  }

  // 设置registry地址，默认为npm官方registry
  const registry = program.registry || 'https://registry.npmjs.org';

  try {
    // 删除dist/packages目录
    await fse.remove('../dist/packages');

    // 如果未跳过版本验证，执行版本验证
    if (!program.skipVersionValidation) {
      await execa(
        'yarn',
        [
          'validate-versions',
          '--compareVersion',
          program.nextVersion,
          '--path',
          '../ng-packs/packages',
        ],
        { stdout: 'inherit', cwd: '../../scripts' },
      );
    }

    // 如果是预览发布，执行替换预览版本操作
    if (program.preview) await replaceWithPreview(program.nextVersion);

    // 执行构建命令
    await execa('yarn', ['build', '--noInstall', '--skipNgcc'], { stdout: 'inherit' });
    await execa('yarn', ['build:schematics'], { stdout: 'inherit' });
  } catch (error) {
    // 处理错误，回滚版本并退出
    console.error(error.stderr);
    console.error('\n\nAn error has occurred! Rolling back the changed package versions.');
    await updateVersion(oldVersion);
    process.exit(1);
  }

  try {
    // 重命名lerna配置文件
    await fse.rename('../lerna.publish.json', '../lerna.json');

    // 确定发布标签
    let tag: string;
    if (program.preview) tag = 'preview';
    else if (semverParse(program.nextVersion).prerelease?.length) tag = 'next';

    // 使用lerna执行npm发布
    await execa(
      'yarn',
      ['lerna', 'exec', '--', `"npm publish --registry ${registry}${tag ? ` --tag ${tag}` : ''}"`],
      {
        stdout: 'inherit',
        cwd: '../',
      },
    );

    // 恢复lerna配置文件
    await fse.rename('../lerna.json', '../lerna.publish.json');
  } catch (error) {
    // 处理发布错误
    console.error(error.stderr);
    console.error('\n\nAn error has occurred while publishing to the NPM!');
    await fse.rename('../lerna.json', '../lerna.publish.json');
    process.exit(1);
  }

  try {
    // 如果不是预览发布且未跳过git操作，执行git提交
    if (!program.preview && !program.skipGit) {
      await execa('git', ['add', '../packages/*', '../package.json', '../lerna.version.json'], {
        stdout: 'inherit',
      });
      await execa('git', ['commit', '-m', 'Upgrade ng package versions', '--no-verify'], {
        stdout: 'inherit',
      });
    }
  } catch (error) {
    // 处理git操作错误
    console.error(error.stderr);
    process.exit(1);
  }

  // 正常退出
  process.exit(0);
})();

// 定义更新版本的异步函数
async function updateVersion(version: string) {
  // 执行版本更新命令
  await execa(
    'yarn',
    ['update-version', version],
    { stdout: 'inherit', cwd: '../' },
  );

  // 执行替换为波浪号版本命令
  await execa('yarn', ['replace-with-tilde']);
}