import execa from 'execa';  // 导入execa库，用于执行命令行命令
import fse from 'fs-extra'; // 导入fs-extra库，提供增强的文件系统操作

(async () => {
  // 1. 在上级目录执行yarn ng build --prod命令，进行生产环境构建
  await execa('yarn', ['ng', 'build', '--prod','--verbose'], {
    stdout: 'inherit',  // 将子进程的输出直接显示在父进程中
    cwd: '..',          // 设置工作目录为上级目录
  });
  // 2. 在模板的angular目录下执行yarn install --ignore-scripts命令
  await execa('yarn', ['install', '--ignore-scripts'], {
    stdout: 'inherit',
    cwd: '../../../templates/app/angular',  // 设置工作目录为模板的angular目录
  });
  // 3. 删除模板angular目录下的@dignite-ng模块
  await fse.remove('../../../templates/app/angular/node_modules/@dignite-ng');

  // 4. 将本地的@dignite-ng模块复制到模板的angular目录下
  await fse.copy('../node_modules/@dignite-ng', '../../../templates/app/angular/node_modules/@dignite-ng', {
    overwrite: true,  // 如果目标存在则覆盖
  });

  // 5. 在模板的angular目录下再次执行yarn ng build --prod命令
  await execa('yarn', ['ng', 'build', '--prod'], {
    stdout: 'inherit',
    cwd: '../../../templates/app/angular',
  });
})();  // 立即执行这个异步函数