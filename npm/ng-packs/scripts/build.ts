// 1. 引入依赖模块
import program from 'commander'; // 命令行参数解析工具
import execa from 'execa'; // 子进程执行工具
import fse from 'fs-extra'; // 增强版文件操作库

// 2. 立即执行异步函数
(async () => {
  // 3. 配置命令行参数
  program.option('-i, --noInstall', '跳过package.json更新和依赖安装', false);
  program.option('-c, --skipNgcc', '跳过Angular兼容性编译', false);

  program.parse(process.argv); // 解析命令行参数
 
  try {
    // 4. 依赖安装控制
    if (!program.noInstall) {
      await execa('yarn', ['install'], {
        stdout: 'inherit', // 继承父进程输出
        cwd: '../', // 在上级目录执行（项目根目录）
      });
    }

    // 5. 清理旧构建产物
    await fse.remove('../dist');
    console.error('清理旧构建产物');
    // 6. 分阶段执行NX构建命令
    // 第一阶段构建核心模块
    await execa(
      'yarn',
      [
        'nx', // 使用NX构建工具
        'run-many', // 多项目构建命令
        '--target',
        'build', // 执行package.json中定义的build任务
        '--prod', // 生产模式（启用代码优化）
        '--projects',
        'core', // 仅构建core核心模块
        '--parallel',
        '1', // 单线程构建（确保执行顺序）
        '--skip-nx-cache', // 禁用构建缓存（强制全新构建）
        "--verbose", // 输出详细日志
      ],
      { cwd: '../' }, // 在项目根目录执行
    );
    // 第二阶段构建账户/权限相关模块
    await execa(
      'yarn',
      [
        'nx',
        'run-many', // 使用NX工具的多项目构建
        '--target',
        'build', // 执行构建任务
        '--prod', // 生产模式（启用优化）
        '--projects', // 指定构建以下项目：
        'tenant-domain-management,regionalization-management,ck-editor,file-explorer,dynamic-form', // 权限/账户核心模块
        '--parallel',
        '1', // 单线程顺序执行
        '--skip-nx-cache', // 禁用缓存（强制全新构建）
        "--verbose", // 输出详细日志
      ],
      { cwd: '../' },
    ); // 在项目根目录执行

    // 第三阶段构模块（排除已构建的和dev/schematics）
    await execa(
      'yarn',
      [
        'nx',
        'run-many', // 使用NX工具的多项目构建
        '--target',
        'build', // 执行构建任务
        '--prod', // 生产模式（启用优化）
        '--projects', // 指定构建以下项目：
        'cms', // 权限/账户核心模块
        '--parallel',
        '1', // 单线程顺序执行
        '--skip-nx-cache', // 禁用缓存（强制全新构建）
        "--verbose", // 输出详细日志
      ],
      { cwd: '../' },
    ); // 在项目根目录执行


    // // 第四阶段构建剩余模块（排除已构建的和dev/schematics）
    // await execa(
    //   'yarn',
    //   [
    //     'nx',
    //     'run-many', // 使用NX工具执行多项目构建
    //     '--target',
    //     'build', // 指定构建任务
    //     '--prod', // 生产模式（启用优化）
    //     '--all', // 构建所有项目
    //     '--exclude', // 排除以下项目:
    //     'dev-app,schematics,core,tenant-domain-management,regionalization-management,ck-editor,file-explorer,dynamic-form', // 注意core后的多余空格（可能需要修正）
    //     '--parallel',
    //     '1', // 单进程执行（保证构建顺序）
    //     '--skip-nx-cache', // 禁用NX缓存（强制全新构建）
    //   ],
    //   { cwd: '../' },
    // ); // 在项目根目录执行

    // 7. 执行Angular兼容性编译
    if (!program.skipNgcc) {
      await execa('yarn', ['compile:ivy'], {
        stdout: 'inherit',
        cwd: '../',
      });
    }
  } catch (error) {
    // 8. 异常处理
    console.error('异常处理',error);
    process.exit(1); // 非零退出码表示失败
  }

  process.exit(0); // 成功退出
})();
