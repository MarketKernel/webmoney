using System.Runtime.InteropServices;

namespace WebMoney.Cryptography
{
    [ComVisible(true)]
    public interface ISigner
    {
        void Initialize(KeeperKey keeperKey);
        string Sign(string value);
    }
}
