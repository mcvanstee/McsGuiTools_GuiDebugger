namespace IRL_Gui_Debugger.DisplaySimulator
{
    public enum DataType
    {
        RLE,
        RLE_Alpha,
    }

    public class DataLocation
    {
        public int Id { get; set; }
        public DataType Type { get; set; }

        public DataLocation(int id, DataType type)
        {
            Id = id;
            Type = type;
        }
    }
}
