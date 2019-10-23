using Arcanoid.Models;

namespace Arcanoid.Views
{
    public interface IBlocksView
    {
        void Remove();
        Block GetBlockInfo();
    }
}
