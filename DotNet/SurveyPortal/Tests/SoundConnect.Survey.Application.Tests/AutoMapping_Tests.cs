﻿using System.Collections.Generic;
using Shouldly;
using NUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoundConnect.Survey.Application.Tests
{
    [TestClass]
    public class AutoMapping_Tests
    {
        static AutoMapping_Tests()
        {

        }

        //TestEXAMPLES
        /*
        [Fact]
        public void MapTo_Tests()
        {
            var obj1 = new MyClass1 { TestProp = "Test value" };

            var obj2 = obj1.MapTo<MyClass2>();
            obj2.TestProp.ShouldBe("Test value");

            var obj3 = obj1.MapTo<MyClass3>();
            obj3.TestProp.ShouldBe("Test value");
        }

        [Fact]
        public void MapTo_Existing_Object_Tests()
        {
            var obj1 = new MyClass1 { TestProp = "Test value" };

            var obj2 = new MyClass2();
            obj1.MapTo(obj2);
            obj2.TestProp.ShouldBe("Test value");

            var obj3 = new MyClass3();
            obj2.MapTo(obj3);
            obj3.TestProp.ShouldBe("Test value");
        }

        [Fact]
        public void MapFrom_Tests()
        {
            var obj2 = new MyClass2 { TestProp = "Test value" };

            var obj1 = obj2.MapTo<MyClass1>();
            obj1.TestProp.ShouldBe("Test value");
        }

        [Fact]
        public void MapTo_Collection_Tests()
        {
            var list1 = new List<MyClass1>
                        {
                            new MyClass1 {TestProp = "Test value 1"},
                            new MyClass1 {TestProp = "Test value 2"}
                        };

            var list2 = list1.MapTo<List<MyClass2>>();
            list2.Count.ShouldBe(2);
            list2[0].TestProp.ShouldBe("Test value 1");
            list2[1].TestProp.ShouldBe("Test value 2");
        }*/

    }
}
