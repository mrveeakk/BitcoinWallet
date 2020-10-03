using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using NBitcoin;
using NBitcoin.OpenAsset;
using NBitcoin.Tests;


namespace NBitcoinFirst
{
    class BitcoinWallet
    {
        public static void CreateWallet()
        {
            var secret = new BitcoinSecret("L3E7oUgKrmF4ED4xTSKem3NjduRcCVNmfG59wTG3p28YGkY9a5og");
            ExtKey masterKey = new ExtKey();
            Transaction tx;
            var input = new TxIn();
            //input.PrevOut = new OutPoint(new uint256("1CxZnG2Mb31YHcsoKx18yshWdbcM9Y1mb5"),0);
            Coin y = new Coin();
            y.Amount = 5;

            //ColoredTransaction coloredTransaction = new ColoredTransaction();
            Console.WriteLine("Address: " + secret.PubKey.GetAddress(Network.Main));
            Console.WriteLine("Key: " + masterKey.GetWif(Network.Main));
            Console.WriteLine("Master key : " + masterKey.ToString(Network.Main));
            for (int i = 0; i < 5; i++)
            {
                ExtKey key2 = masterKey.Derive((uint)i);
                Console.WriteLine("Key " + i + " : " + key2.ToString(Network.Main));
            }

            ExtKey extKey = new ExtKey();
            byte[] chainCode = extKey.ChainCode;
            Key key = extKey.PrivateKey;

            ExtKey newExtKey = new ExtKey(key, chainCode);


            ExtPubKey masterPubKey = masterKey.Neuter();
            for (int i = 0; i < 5; i++)
            {
                ExtPubKey pubkey = masterPubKey.Derive((uint)i);
                Console.WriteLine("PubKey " + i + " : " + pubkey.ToString(Network.Main));
            }

            masterKey = new ExtKey();
            masterPubKey = masterKey.Neuter();


            ExtPubKey pubkey1 = masterPubKey.Derive((uint)1);


            ExtKey key1 = masterKey.Derive((uint)1);


            Console.WriteLine("Generated address : " + pubkey1.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main));
            Console.WriteLine("Expected address : " + key1.PrivateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main));

            ExtKey parent = new ExtKey();
            ExtKey child11 = parent.Derive(new KeyPath("1/1"));

            ExtKey Key = new ExtKey();
            Console.WriteLine("Key: " + Key.ToString(Network.Main));
            ExtKey accountingKey = Key.Derive(0, hardened: false);

            ExtPubKey ceoPubkey = Key.Neuter();

            ExtKey ceoKeyRecovered = accountingKey.GetParentExtKey(ceoPubkey);
            Console.WriteLine("Key recovered: " + ceoKeyRecovered.ToString(Network.Main));
            var nonHardened = new KeyPath("1/2/3");
            var hardened = new KeyPath("1/2/3'");
            Key = new ExtKey();
            string accounting = "1'";
            int customerId = 5;
            int paymentId = 50;
            KeyPath path = new KeyPath(accounting + "/" + customerId + "/" + paymentId);
            ExtKey paymentKey = Key.Derive(path);
        }
       
    }
}
