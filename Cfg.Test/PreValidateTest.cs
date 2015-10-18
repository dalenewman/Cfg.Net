﻿using System.Collections.Generic;
using System.Linq;
using Cfg.Net;
using Cfg.Net.Contracts;
using Cfg.Net.Ext;
using NUnit.Framework;

namespace Cfg.Test {

    [TestFixture]
    public class PreValidateTest {

        [Test]
        public void TestPreValidateAProperty() {
            const string resource = @"<cfg>
                <things>
                    <add name='one' value='something' />
                    <add name='two' value='Another' />
                </things>
            </cfg>";
            var actual = new TestProperty(resource, new TraceLogger());
            Assert.AreEqual(0, actual.Errors().Length);
            Assert.AreEqual(2, actual.Things.Count);
        }


        [Test]
        public void TestPreValidateACollection() {
            const string resource = @"<cfg>
                <things>
                    <add name='one' value='something' />
                    <add name='two' value='Another' />
                </things>
            </cfg>";
            var actual = new TestCollection(resource, new TraceLogger());
            Assert.AreEqual(1, actual.Errors().Length);
            Assert.AreEqual(3, actual.Things.Count);
        }

        private class TestProperty : CfgNode {
            public TestProperty(string cfg, IDependency logger)
                : base(logger) {
                Load(cfg);
            }
            [Cfg]
            public List<Thing> Things { get; set; }
        }

        private class Thing : CfgNode {
            [Cfg]
            public string Name { get; set; }

            [Cfg(domain = "Something,Another", ignoreCase = false)]
            public string Value { get; set; }

            protected override void PreValidate() {
                if (char.IsLower(Value[0])) {
                    Value = char.ToUpper(Value[0]) + Value.Substring(1);
                }
            }
        }

        private class TestCollection : CfgNode {
            public TestCollection(string cfg, ILogger logger)
                : base(logger) {
                Load(cfg);
            }
            [Cfg]
            public List<Thing> Things { get; set; }

            protected override void PreValidate() {
                var thing = new Thing { Name = "three", Value = "error" }.WithValidation();
                if (thing.Errors().Any()) {
                    foreach (var error in thing.Errors()) {
                        Error(error);
                    }
                }
                Things.Add(thing);
            }
        }

    }
}
