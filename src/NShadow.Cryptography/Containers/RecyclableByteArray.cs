using System.Buffers;

namespace NShadow.Cryptography.Containers;

public sealed class RecyclableByteArray : IDisposable
{
    public byte[] Array { get; }

    public int SignificantLength { get; private set; }

    public static RecyclableByteArray Rent(int minimumLength)
    {
        var array = ArrayPool<byte>.Shared.Rent(minimumLength);
        return new RecyclableByteArray(array);
    }

    private RecyclableByteArray(byte[] arr, int significantLength = 0)
    {
        Array = arr;
        SignificantLength = significantLength;
    }

    public void Dispose()
    {
        if (SignificantLength < 1)
        {
            return;
        }

        ArrayPool<byte>.Shared.Return(Array);
        SignificantLength = 0;
        GC.SuppressFinalize(this);
    }

    ~RecyclableByteArray()
    {
        Dispose();
    }
}