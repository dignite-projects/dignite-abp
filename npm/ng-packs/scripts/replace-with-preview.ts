import fse from 'fs-extra';  // 导入fs-extra库，用于文件系统操作

// 导出默认的异步函数，接收一个version参数
export default async function(version: string) {
  // 定义core包的package.json文件路径
  const corePkgPath = '../packages/core/package.json';
  
  // 使用try-catch捕获可能的错误
  try {
    // 读取并解析core包的package.json文件
    const corePkg = await fse.readJSON(corePkgPath);

    // 将修改后的内容写回package.json文件
    await fse.writeJSON(
      corePkgPath,  // 文件路径
      {
        ...corePkg,  // 保留原有内容
        dependencies: { 
          ...corePkg.dependencies,  // 保留原有依赖
          '@abp/utils': version     // 更新或添加@abp/utils的版本 @abp/utils存在pack\utils目录下，不需要更改名称
        },
      },
      { spaces: 2 },  // 使用2个空格进行格式化
    );
  } catch (error) {
    // 如果发生错误，打印错误信息
    console.error(error);
  }
}