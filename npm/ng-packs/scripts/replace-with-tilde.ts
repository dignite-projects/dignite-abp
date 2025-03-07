import fse from 'fs-extra';  // 导入fs-extra库，用于文件系统操作
import glob from 'glob';    // 导入glob库，用于文件模式匹配

// 定义异步函数replace，用于处理单个package.json文件
async function replace(filePath: string) {
  // 读取并解析package.json文件
  const pkg = await fse.readJson(filePath);

  // 解构获取dependencies对象
  const { dependencies } = pkg;

  // 如果没有dependencies则直接返回
  if (!dependencies) return;

  // 遍历dependencies的所有键
  Object.keys(dependencies).forEach(key => {
    // 如果包名包含@dignite-ng/
    if (key.includes('@dignite-ng/')) {
      // 将版本号前的^符号替换为~
      dependencies[key] = dependencies[key].replace('^', '~');
    }
  });

  // 将修改后的内容写回package.json文件，保留2个空格的缩进
  await fse.writeJson(filePath, { ...pkg, dependencies }, { spaces: 2 });
}

// 使用glob查找所有package.json文件
glob('../packages/**/package.json', {}, (er, files) => {
  // 遍历找到的所有文件路径
  files.forEach(path => {
    // 如果路径包含node_modules则跳过
    if (path.includes('node_modules')) {
      return;
    }

    // 对每个符合条件的package.json文件执行replace操作
    replace(path);
  });
});