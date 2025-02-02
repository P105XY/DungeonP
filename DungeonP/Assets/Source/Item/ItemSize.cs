namespace ItemNameSpace
{
    public struct ItemSize
    {
        private int ItemXSpace;
        private int ItemYSpace;

        public ItemSize(int InItemXSpace, int InItemYSpace)
        {
            this.ItemXSpace = InItemXSpace;
            this.ItemYSpace = InItemYSpace;
        }

        public int GetItemXSpace()
        {
            return this.ItemXSpace;
        }

        public int GetItemYSpace()
        {
            return this.ItemYSpace;
        }
    }
}