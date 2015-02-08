using System.IO;
using NUnit.Framework;
using ProtoBuf;
using ProtoBuf.Meta;
using TestSolution.Tests.Common;

namespace TestSolution.Tests.Protobuf.Serialization
{
    public class BasicTests : BaseTestClass
    {

        private class BasicSerializableClass
        {
            public int Number { get; set; }
            public string Name { get; set; }
        }

        private class NormalSerializableClass
        {
            public BasicSerializableClass Component { get; set; }
            public int ID { get; set; }
        }

        private class DerivedSerializableClass : BasicSerializableClass
        {
            public int Value { get; set; }
        }

        public override void TestFixtureSetUp()
        {
            var metaType = RuntimeTypeModel.Default.Add(typeof(BasicSerializableClass), true);
            metaType.Add(1, "Number");
            metaType.Add(2, "Name");
            metaType.AddSubType(3, typeof (DerivedSerializableClass));

            var derivedMetaType = RuntimeTypeModel.Default.Add(typeof(DerivedSerializableClass), true);
            derivedMetaType.Add(1, "Value");

            var normalMetaType = RuntimeTypeModel.Default.Add(typeof(NormalSerializableClass), true);
            normalMetaType.Add(1, "Component");
            normalMetaType.Add(2, "ID");
        }

        private static void AssertBasicSerializableClassesAreSame(BasicSerializableClass serialized,
            BasicSerializableClass deserialized)
        {
            Assert.AreEqual(serialized.Name, deserialized.Name);
            Assert.AreEqual(serialized.Number, deserialized.Number);
        }

        private static void AssertDerivedSerializableClassesAreSame(DerivedSerializableClass serialized,
            DerivedSerializableClass deserialized)
        {
            AssertBasicSerializableClassesAreSame(serialized, deserialized);
            Assert.AreEqual(serialized.Value, deserialized.Value);
        }

        private static void AssertNormalSerilizableClassesAreSame(NormalSerializableClass serialized,
            NormalSerializableClass deserialized)
        {
            AssertBasicSerializableClassesAreSame(serialized.Component, deserialized.Component);
            Assert.AreEqual(serialized.ID, deserialized.ID);
        }

        [Test]
        public void Protobuf_should_serialize_basic_class()
        {
            var serialized = new BasicSerializableClass() {Number = 10, Name = "Name1"};

            var stream = new MemoryStream();

            Serializer.Serialize(stream, serialized);
            stream.Seek(0, SeekOrigin.Begin);
            var deserialized = Serializer.Deserialize<BasicSerializableClass>(stream);
            
            AssertBasicSerializableClassesAreSame(serialized, deserialized);
        }

        [Test]
        public void Protobuf_should_serialize_derived_class()
        {
            var serialized = new DerivedSerializableClass() { Number = 10, Name = "Name1", Value = 89};

            var stream = new MemoryStream();

            Serializer.Serialize(stream, serialized);
            stream.Seek(0, SeekOrigin.Begin);
            var deserialized = Serializer.Deserialize<DerivedSerializableClass>(stream);

            AssertDerivedSerializableClassesAreSame(serialized, deserialized);
        }

        [Test]
        public void Protobuf_should_serialize_agregated_classes()
        {
            var serialized = new NormalSerializableClass() { Component = new BasicSerializableClass(){Name = "str", Number = 13}, ID = 89675};

            var stream = new MemoryStream();

            Serializer.Serialize(stream, serialized);
            stream.Seek(0, SeekOrigin.Begin);
            var deserialized = Serializer.Deserialize<NormalSerializableClass>(stream);

            AssertNormalSerilizableClassesAreSame(serialized, deserialized);
        }

    }
}
