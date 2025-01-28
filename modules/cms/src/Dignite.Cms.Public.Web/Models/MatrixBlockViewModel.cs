using Dignite.Abp.DynamicForms.Matrix;

namespace Dignite.Cms.Public.Web.Models
{
    public class MatrixBlockViewModel
    {
        public MatrixBlockViewModel(MatrixBlockType type, MatrixBlock block,int index)
        {
            Type = type;
            Block = block;
            Index = index;
        }

        public MatrixBlockType Type { get; private set; }
        public MatrixBlock Block { get; private set; }

        public int Index { get; private set; }
    }
}
