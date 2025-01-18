namespace ItemNameSpace
{
    public struct ItemSpace
    {
        private byte ItemXSpace;
        private byte ItemYSpace;

        public ItemSpace(byte InItemXSpace, byte InItemYSpace)
        {
            this.ItemXSpace = InItemXSpace;
            this.ItemYSpace = InItemYSpace;
        }

        public byte GetItemXSpace()
        {
            return this.ItemXSpace;
        }

        public byte GetItemYSpace()
        {
            return this.ItemYSpace;
        }
    }
}