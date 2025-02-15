﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using static NHM.CredentialValidators.CredentialValidators;

namespace NHM.CredentialValidatorsTests
{
    [TestClass]
    public class CredentialValidatorsTests
    {
        internal class TestLabel
        {
            private int _testCount = 0;
            public string label() => $"#{++_testCount}";
        }

        [TestMethod]
        public void TestValidateWorkerName()
        {
            Assert.AreEqual(true, ValidateWorkerName("worker1"));
            Assert.AreEqual(false, ValidateWorkerName("wor ker1"), "Whitespace");
            Assert.AreEqual(false, ValidateWorkerName("worker12345678910123456789"), "too long");
        }

        [TestMethod]
        public void TestValidateBitcoinAddressBase58()
        {
            var tl = new TestLabel { };
            bool isValid(string btc, bool isProduction) => ValidateBitcoinAddressBase58(btc, isProduction);
            Assert.AreEqual(true,  isValid("17VZNX1SN5NtKa8UQFxwQbFeFc3iqRYhem", true), tl.label());
            Assert.AreEqual(true,  isValid("33hGFJZQAfbdzyHGqhJPvZwncDjUBdZqjW", true), tl.label());
            Assert.AreEqual(false, isValid("2N6ibfrTwUSSvzAz1esPe1gYULG82asTHiS", true), tl.label());
            Assert.AreEqual(true,  isValid("2N6ibfrTwUSSvzAz1esPe1gYULG82asTHiS", false), tl.label());
            Assert.AreEqual(false, isValid("whatever", false), tl.label());
            Assert.AreEqual(false, isValid("", false), tl.label());
            Assert.AreEqual(false, isValid(" ", false), tl.label());
            Assert.AreEqual(false, isValid(null, false), tl.label());
            Assert.AreEqual(false, isValid("bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t4", true), tl.label());
            Assert.AreEqual(false, isValid("tb1qw508d6qejxtdg4y5r3zarvary0c5xw7kxpjzsx", false), tl.label());
            Assert.AreEqual(false, isValid("tb1qw508d6qejxtdg4y5r3zarvary0c5xw7kxpjzsc", false), tl.label());
            Assert.AreEqual(false, isValid("tc1qw508d6qejxtdg4y5r3zarvary0c5xw7kg3g4ty", false), tl.label());
            Assert.AreEqual(false, isValid("bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t5", true), tl.label());
        }

        [TestMethod]
        public void TestValidateBitcoinAddressBech32()
        {
            var tl = new TestLabel { };
            bool isValid(string btc, bool isProduction) => ValidateBitcoinAddressBech32(btc, isProduction);
            Assert.AreEqual(false, isValid("17VZNX1SN5NtKa8UQFxwQbFeFc3iqRYhem", true), tl.label());
            Assert.AreEqual(false, isValid("33hGFJZQAfbdzyHGqhJPvZwncDjUBdZqjW", true), tl.label());
            Assert.AreEqual(false, isValid("2N6ibfrTwUSSvzAz1esPe1gYULG82asTHiS", true), tl.label());
            Assert.AreEqual(false, isValid("2N6ibfrTwUSSvzAz1esPe1gYULG82asTHiS", false), tl.label());
            Assert.AreEqual(false, isValid("whatever", false), tl.label());
            Assert.AreEqual(false, isValid("", false), tl.label());
            Assert.AreEqual(false, isValid(" ", false), tl.label());
            Assert.AreEqual(false, isValid(null, false), tl.label());
            Assert.AreEqual(true, isValid("bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t4", true), tl.label());
            Assert.AreEqual(true, isValid("tb1qw508d6qejxtdg4y5r3zarvary0c5xw7kxpjzsx", false), tl.label());
            Assert.AreEqual(false, isValid("tb1qw508d6qejxtdg4y5r3zarvary0c5xw7kxpjzsc", false), tl.label());
            Assert.AreEqual(false, isValid("tc1qw508d6qejxtdg4y5r3zarvary0c5xw7kg3g4ty", false), tl.label());
            Assert.AreEqual(false, isValid("bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t5", true), tl.label());
        }

        [TestMethod]
        public void TestValidateNiceHashMiningAddress()
        {
            var tl = new TestLabel { };
            bool isValid(string btc, bool isProduction) => ValidateMiningAddress(btc, isProduction);
            Assert.AreEqual(true, isValid("NHbHJfBFKbm5Uk9HShpgCnbPsCMctkf6tSfP", true), tl.label());
            Assert.AreEqual(true, isValid("PTbHJfBFKbm5Uk9HShpgCnbPsCMctkf6tSfP", false), tl.label());
            Assert.AreEqual(false, isValid("whatever", false), tl.label());
            Assert.AreEqual(false, isValid("", false), tl.label());
            Assert.AreEqual(false, isValid(" ", false), tl.label());
            Assert.AreEqual(false, isValid(null, false), tl.label());
            Assert.AreEqual(false, isValid("NHbHJfBFKbm5Uk9HShpgCnbPsCMctkf6tSfP", false), tl.label());
            Assert.AreEqual(false, isValid("PTbHJfBFKbm5Uk9HShpgCnbPsCMctkf6tSfP", true), tl.label());
            Assert.AreEqual(true, isValid("PTbD872cNDn2Z2Y3m57u8FrDdqDtnTSx7AuW", false), tl.label());
            Assert.AreEqual(false, isValid("PTbD872cNDn2Z2Y3m57u8FrDdqDtnTSx7AuW", true), tl.label());
            Assert.AreEqual(true, isValid("PTbUDN5nPLDUA34X1aLR24pfuidFZvGRjvhn", false), tl.label());
            Assert.AreEqual(false, isValid("PTbUDN5nPLDUA34X1aLR24pfuidFZvGRjvhn", true), tl.label());
        }

        [TestMethod]
        public void TestValidateBitcoinAddress()
        {
            var tl = new TestLabel{ };
            bool isValid(string btc, bool isProduction) => ValidateBitcoinAddress(btc, isProduction);
            Assert.AreEqual(true,  isValid("33hGFJZQAfbdzyHGqhJPvZwncDjUBdZqjW", true), tl.label());
            Assert.AreEqual(false, isValid("2N6ibfrTwUSSvzAz1esPe1gYULG82asTHiS", true), tl.label());
            Assert.AreEqual(true,  isValid("2N6ibfrTwUSSvzAz1esPe1gYULG82asTHiS", false), tl.label());
            Assert.AreEqual(true,  isValid("whatever", false), tl.label());
            Assert.AreEqual(false, isValid("", false), tl.label());
            Assert.AreEqual(false, isValid(" ", false), tl.label());
            Assert.AreEqual(false, isValid(null, false), tl.label());
            Assert.AreEqual(true, isValid("bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t4", true), tl.label());
            Assert.AreEqual(true, isValid("tb1qw508d6qejxtdg4y5r3zarvary0c5xw7kxpjzsx", false), tl.label());
            Assert.AreEqual(true, isValid("tb1qw508d6qejxtdg4y5r3zarvary0c5xw7kxpjzsc", false), tl.label());
            Assert.AreEqual(true, isValid("tc1qw508d6qejxtdg4y5r3zarvary0c5xw7kg3g4ty", false), tl.label());
            Assert.AreEqual(false, isValid("bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t5", true), tl.label());
            Assert.AreEqual(true, isValid("NHbHJfBFKbm5Uk9HShpgCnbPsCMctkf6tSfP", true), tl.label());
            Assert.AreEqual(true, isValid("PTbHJfBFKbm5Uk9HShpgCnbPsCMctkf6tSfP", false), tl.label());
            Assert.AreEqual(true, isValid("PTbD872cNDn2Z2Y3m57u8FrDdqDtnTSx7AuW", false), tl.label());
            Assert.AreEqual(true, isValid("PTbUDN5nPLDUA34X1aLR24pfuidFZvGRjvhn", false), tl.label());
        }

    }
}
