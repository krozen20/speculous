﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Speculous.Tests
{
    public class TestCaseTests
    {

        public class FuncGivenMethod : TestCase<string>
        {
            protected override Func<string> Given()
            {
                SampleObject.BaseMessage = "Hello there";

                var sample = new SampleObject();

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldPerformGiven()
            {
                Subject().Should().Be("Hello there, test");
            }

            [Fact]
            public void ShouldPerformGivenExecuted()
            {
                Its.Should().Be("Hello there, test");
            }

            //change context
            public class ChangeContext : TestCase<string>
            {
                protected override Func<string> Given()
                {
                    var sample = new SampleObject();
                    return () => sample.GetMessage("mike");
                }

                [Fact]
                public void ShouldChangeContext()
                {
                    Subject().Should().Be("Hello there, mike");
                }
            }

        }

        public class ActionGivenMethod : TestCase
        {
            protected override Action Given()
            {
                SampleObject.BaseMessage = "Hello World!!!";
                var sample = new SampleObject();

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldPerformGiven()
            {
                Subject();
            }

            public class ChangeContext : TestCase
            {
                protected override Action Given()
                {
                    var sample = new SampleObject();

                    return () => sample.GetMessage("mike");
                }

                [Fact]
                public void ShouldPerformGivenContext()
                {
                    Subject();
                    SampleObject.BaseMessage.Should().Be("Hello World!!!");
                }
            }

        }

        public class WithMethod : TestCase
        {
            protected override Action Given()
            {
                With(() => SampleObject.BaseMessage = "Hello",
                     () => SampleObject.BaseMessage = "Goodbye");

                var sample = new SampleObject();

                return () => sample.GetMessage("test");
            }

            [Fact]
            public void ShouldBaseMessageIsHello()
            {
                Subject();
                SampleObject.BaseMessage.Should().Be("Hello");
            }

            [Fact]
            public void ShouldBaseMessageIsGoodbye()
            {
                Subject();
                Dispose();
                SampleObject.BaseMessage.Should().Be("Goodbye");
            }
        }



    }
}