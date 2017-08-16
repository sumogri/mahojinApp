namespace Mahojin
{
    /// <summary>
    /// 魔方陣のセルを持つことを示すインターフェース
    /// </summary>
    public interface IHaveCells
    {
        /// <summary>
        /// セルに入力された数を返すメソッド
        /// </summary>
        /// <returns>セルにセットされた数</returns>
        int?[] GetCells();       
    }
}