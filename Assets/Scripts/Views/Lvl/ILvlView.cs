namespace Arcanoid.Views
{
    public interface ILvlView
    {
        #region Public Properties

        IBlockView[] Blocks { get; }

        void Close();

        #endregion Public Properties
    }
}