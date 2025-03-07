const glob = require("glob");  // 导入glob模块，用于文件模式匹配
const fse = require("fs-extra"); // 导入fs-extra模块，提供增强的文件系统操作

function replace(filePath) {
  const pkg = fse.readJsonSync(filePath); // 同步读取并解析指定路径的package.json文件

  const { dependencies } = pkg; // 从package.json中解构出dependencies对象

  if (!dependencies) return; // 如果没有dependencies，直接返回

  // 遍历dependencies的每个键值对
  Object.keys(dependencies).forEach((key) => {
    // 如果包名包含"@dignite-ng/"
    if (key.includes("@dignite-ng/")) {
      // 将版本号前的^替换为~
      dependencies[key] = dependencies[key].replace("^", "~");
    }
  });

  // 将修改后的内容写回原文件，保留其他字段，缩进为2个空格
  fse.writeJsonSync(filePath, { ...pkg, dependencies }, { spaces: 2 });
}

// 使用glob匹配所有packs目录下的package.json文件
glob("./packs/**/package.json", {}, (er, files) => {
  // 遍历找到的所有文件路径
  files.forEach((path) => {
    // 如果路径包含node_modules，跳过
    if (path.includes("node_modules")) {
      return;
    }

    // 对符合条件的文件执行replace操作
    replace(path);
  });
});