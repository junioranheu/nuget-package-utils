namespace junioranheu_utils_package.Entities.Output
{
    public sealed class StreamingFileOutput
    {
        public double PorcentagemCompleta { get; set; }

        public required byte[] Chunk { get; set; }
    }
}