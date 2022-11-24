using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Entitas.Tests
{
    public class MatcherTests
    {
        readonly Test1Entity _eA;
        readonly Test1Entity _eB;
        readonly Test1Entity _eC;
        readonly Test1Entity _eAB;
        readonly Test1Entity _eABC;

        public MatcherTests()
        {
            _eA = CreateEntity();
            _eA.AddComponentA();

            _eB = CreateEntity();
            _eB.AddComponentB();

            _eC = CreateEntity();
            _eC.AddComponentC();

            _eAB = CreateEntity();
            _eAB.AddComponentA();
            _eAB.AddComponentB();

            _eABC = CreateEntity();
            _eABC.AddComponentA();
            _eABC.AddComponentB();
            _eABC.AddComponentC();
        }

        [Fact]
        public void AllOfHasAllIndexes()
        {
            var matcher = CreateAllOfAB();
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
        }

        [Fact]
        public void AnyOfHasAllIndexes()
        {
            var matcher = CreateAnyOfAB();
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.AnyOfIndexes, CID.ComponentA, CID.ComponentB);
        }

        [Fact]
        public void AllOfNoneOfHasAllIndexes()
        {
            var matcher = CreateAllOfABNoneOfCD();
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC, CID.ComponentD);
            AssertIndexesEqual(matcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.NoneOfIndexes, CID.ComponentC, CID.ComponentD);
        }

        [Fact]
        public void AnyOfNoneOfHasAllIndexes()
        {
            var matcher = CreateAnyOfABNoneOfCD();
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC, CID.ComponentD);
            AssertIndexesEqual(matcher.AnyOfIndexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.NoneOfIndexes, CID.ComponentC, CID.ComponentD);
        }

        [Fact]
        public void AllOfAnyOfHasAllIndexes()
        {
            var matcher = CreateAllOfABAnyOfCD();
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC, CID.ComponentD);
            AssertIndexesEqual(matcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.AnyOfIndexes, CID.ComponentC, CID.ComponentD);
        }

        [Fact]
        public void AllOfHasAllIndexesWithoutDuplicates()
        {
            var matcher = Matcher<Test1Entity>.AllOf(CID.ComponentA, CID.ComponentA, CID.ComponentB, CID.ComponentB);
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
        }

        [Fact]
        public void AnyOfHasAllIndexesWithoutDuplicates()
        {
            var matcher = Matcher<Test1Entity>.AnyOf(CID.ComponentA, CID.ComponentA, CID.ComponentB, CID.ComponentB);
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.AnyOfIndexes, CID.ComponentA, CID.ComponentB);
        }

        [Fact]
        public void AllOfNoneOfHasAllIndexesWithoutDuplicates()
        {
            var matcher = Matcher<Test1Entity>
                .AllOf(CID.ComponentA, CID.ComponentA, CID.ComponentB)
                .NoneOf(CID.ComponentB, CID.ComponentC, CID.ComponentC);
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
            AssertIndexesEqual(matcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.NoneOfIndexes, CID.ComponentB, CID.ComponentC);
        }

        [Fact]
        public void AnyOfNoneOfHasAllIndexesWithoutDuplicates()
        {
            var matcher = Matcher<Test1Entity>
                .AnyOf(CID.ComponentA, CID.ComponentA, CID.ComponentB)
                .NoneOf(CID.ComponentB, CID.ComponentC, CID.ComponentC);
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
            AssertIndexesEqual(matcher.AnyOfIndexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.NoneOfIndexes, CID.ComponentB, CID.ComponentC);
        }

        [Fact]
        public void AllOfAnyOfHasAllIndexesWithoutDuplicates()
        {
            var matcher = Matcher<Test1Entity>
                .AllOf(CID.ComponentA, CID.ComponentA, CID.ComponentB)
                .AnyOf(CID.ComponentB, CID.ComponentC, CID.ComponentC);
            AssertIndexesEqual(matcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
            AssertIndexesEqual(matcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(matcher.AnyOfIndexes, CID.ComponentB, CID.ComponentC);
        }

        [Fact]
        public void AllOfCachesIndexes()
        {
            var matcher = CreateAllOfAB();
            matcher.Indexes.Should().BeSameAs(matcher.Indexes);
        }

        [Fact]
        public void AnyOfCachesIndexes()
        {
            var matcher = CreateAnyOfAB();
            matcher.Indexes.Should().BeSameAs(matcher.Indexes);
        }

        [Fact]
        public void AllOfNoneOfCachesIndexes()
        {
            var matcher = CreateAllOfABNoneOfCD();
            matcher.Indexes.Should().BeSameAs(matcher.Indexes);
        }

        [Fact]
        public void AnyOfNoneOfCachesIndexes()
        {
            var matcher = CreateAnyOfABNoneOfCD();
            matcher.Indexes.Should().BeSameAs(matcher.Indexes);
        }

        [Fact]
        public void AllOfAnyOfCachesIndexes()
        {
            var matcher = CreateAllOfABAnyOfCD();
            matcher.Indexes.Should().BeSameAs(matcher.Indexes);
        }

        [Fact]
        public void AllOfDoesNotMatch()
        {
            CreateAllOfAB().Matches(_eA).Should().BeFalse();
        }

        [Fact]
        public void AnyOfDoesNotMatch()
        {
            CreateAnyOfAB().Matches(_eC).Should().BeFalse();
        }

        [Fact]
        public void AllOfNoneOfDoesNotMatch()
        {
            CreateAllOfABNoneOfCD().Matches(_eABC).Should().BeFalse();
        }

        [Fact]
        public void AnyOfNoneOfDoesNotMatch()
        {
            CreateAnyOfABNoneOfCD().Matches(_eABC).Should().BeFalse();
        }

        [Fact]
        public void AllOfAnyOfDoesNotMatch()
        {
            CreateAllOfABAnyOfCD().Matches(_eAB).Should().BeFalse();
        }

        [Fact]
        public void AllOfMatches()
        {
            var matcher = CreateAllOfAB();
            matcher.Matches(_eAB).Should().BeTrue();
            matcher.Matches(_eABC).Should().BeTrue();
        }

        [Fact]
        public void AnyOfMatches()
        {
            var matcher = CreateAnyOfAB();
            matcher.Matches(_eA).Should().BeTrue();
            matcher.Matches(_eB).Should().BeTrue();
            matcher.Matches(_eABC).Should().BeTrue();
        }

        [Fact]
        public void AllOfNoneOfMatches()
        {
            CreateAllOfABNoneOfCD().Matches(_eAB).Should().BeTrue();
        }

        [Fact]
        public void AnyOfNoneOfMatches()
        {
            var matcher = CreateAnyOfABNoneOfCD();
            matcher.Matches(_eA).Should().BeTrue();
            matcher.Matches(_eB).Should().BeTrue();
        }

        [Fact]
        public void AllOfAnyOfMatches()
        {
            CreateAllOfABAnyOfCD().Matches(_eABC).Should().BeTrue();
        }

        [Fact]
        public void AllOfMergesMatchersToNewMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AllOf(CID.ComponentC);
            var mergedMatcher = Matcher<Test1Entity>.AllOf(m1, m2, m3);
            AssertIndexesEqual(mergedMatcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
            AssertIndexesEqual(mergedMatcher.AllOfIndexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
        }

        [Fact]
        public void AnyOfMergesMatchersToNewMatcher()
        {
            var m1 = Matcher<Test1Entity>.AnyOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AnyOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AnyOf(CID.ComponentC);
            var mergedMatcher = Matcher<Test1Entity>.AnyOf(m1, m2, m3);
            AssertIndexesEqual(mergedMatcher.Indexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
            AssertIndexesEqual(mergedMatcher.AnyOfIndexes, CID.ComponentA, CID.ComponentB, CID.ComponentC);
        }

        [Fact]
        public void AllOfMergesMatchersToNewMatcherWithoutDuplicates()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m3 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var mergedMatcher = Matcher<Test1Entity>.AllOf(m1, m2, m3);
            AssertIndexesEqual(mergedMatcher.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(mergedMatcher.AllOfIndexes, CID.ComponentA, CID.ComponentB);
        }

        [Fact]
        public void AnyOfMergesMatchersToNewMatcherWithoutDuplicates()
        {
            var m1 = Matcher<Test1Entity>.AnyOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AnyOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AnyOf(CID.ComponentB);
            var mergedMatcher = Matcher<Test1Entity>.AnyOf(m1, m2, m3);
            AssertIndexesEqual(mergedMatcher.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(mergedMatcher.AnyOfIndexes, CID.ComponentA, CID.ComponentB);
        }

        [Fact]
        public void AllOfThrowsWhenMergingMatcherWithMoreThanOneIndex()
        {
            var matcher = Matcher<Test1Entity>.AllOf(CID.ComponentA, CID.ComponentB);
            FluentActions.Invoking(() => Matcher<Test1Entity>.AllOf(matcher))
                .Should().Throw<MatcherException>();
        }

        [Fact]
        public void AnyOfThrowsWhenMergingMatcherWithMoreThanOneIndex()
        {
            var matcher = Matcher<Test1Entity>.AnyOf(CID.ComponentA, CID.ComponentB);
            FluentActions.Invoking(() => Matcher<Test1Entity>.AnyOf(matcher))
                .Should().Throw<MatcherException>();
        }

        [Fact]
        public void AllOfCanToString()
        {
            CreateAllOfAB().ToString().Should().Be("AllOf(1, 2)");
        }

        [Fact]
        public void AnyOfCanToString()
        {
            CreateAnyOfAB().ToString().Should().Be("AnyOf(1, 2)");
        }

        [Fact]
        public void AllOfNoneOfCanToString()
        {
            CreateAllOfABNoneOfCD().ToString().Should().Be("AllOf(1, 2).NoneOf(3, 4)");
        }

        [Fact]
        public void AnyOfNoneOfCanToString()
        {
            CreateAnyOfABNoneOfCD().ToString().Should().Be("AnyOf(1, 2).NoneOf(3, 4)");
        }

        [Fact]
        public void AllOfAnyOfCanToString()
        {
            CreateAllOfABAnyOfCD().ToString().Should().Be("AllOf(1, 2).AnyOf(3, 4)");
        }

        [Fact]
        public void ToStringUsesComponentNamesWhenSet()
        {
            var matcher = (Matcher<Test1Entity>)CreateAllOfAB();
            matcher.ComponentNames = new[] {"one", "two", "three"};
            matcher.ToString().Should().Be("AllOf(two, three)");
        }

        [Fact]
        public void ToStringUsesComponentNamesWhenMergedMatcher()
        {
            var m1 = (Matcher<Test1Entity>)Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = (Matcher<Test1Entity>)Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = (Matcher<Test1Entity>)Matcher<Test1Entity>.AllOf(CID.ComponentC);
            m2.ComponentNames = new[] {"m_0", "m_1", "m_2", "m_3"};
            var mergedMatcher = Matcher<Test1Entity>.AllOf(m1, m2, m3);
            mergedMatcher.ToString().Should().Be("AllOf(m_1, m_2, m_3)");
        }

        [Fact]
        public void AllOfNoneOfToStringUsesComponentNamesWhenComponentNamesSet()
        {
            var matcher = (Matcher<Test1Entity>)CreateAllOfABNoneOfCD();
            matcher.ComponentNames = new[] {"one", "two", "three", "four", "five"};
            matcher.ToString().Should().Be("AllOf(two, three).NoneOf(four, five)");
        }

        [Fact]
        public void NoneOfMutatesAllOfMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = m1.NoneOf(CID.ComponentB);
            m1.Should().BeSameAs(m2);
            AssertIndexesEqual(m1.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(m1.AllOfIndexes, CID.ComponentA);
            AssertIndexesEqual(m1.NoneOfIndexes, CID.ComponentB);
        }

        [Fact]
        public void NoneOfMutatesAllOfMergedMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AllOf(m1);
            var m4 = m3.NoneOf(m2);
            m3.Should().BeSameAs(m4);
            AssertIndexesEqual(m3.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(m3.AllOfIndexes, CID.ComponentA);
            AssertIndexesEqual(m3.NoneOfIndexes, CID.ComponentB);
        }

        [Fact]
        public void NoneOfMutatesAnyOfMatcher()
        {
            var m1 = Matcher<Test1Entity>.AnyOf(CID.ComponentA);
            var m2 = m1.NoneOf(CID.ComponentB);
            m1.Should().BeSameAs(m2);
            AssertIndexesEqual(m1.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(m1.AnyOfIndexes, CID.ComponentA);
            AssertIndexesEqual(m1.NoneOfIndexes, CID.ComponentB);
        }

        [Fact]
        public void NoneOfMutatesAnyOfMergedMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AnyOf(m1);
            var m4 = m3.NoneOf(m2);
            m3.Should().BeSameAs(m4);
            AssertIndexesEqual(m3.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(m3.AnyOfIndexes, CID.ComponentA);
            AssertIndexesEqual(m3.NoneOfIndexes, CID.ComponentB);
        }

        [Fact]
        public void AnyOfMutatesAllOfMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = m1.AnyOf(CID.ComponentB);
            m1.Should().BeSameAs(m2);
            AssertIndexesEqual(m1.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(m1.AllOfIndexes, CID.ComponentA);
            AssertIndexesEqual(m1.AnyOfIndexes, CID.ComponentB);
        }

        [Fact]
        public void AnyOfMutatesAllOfMergedMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AllOf(m1);
            var m4 = m3.AnyOf(m2);
            m3.Should().BeSameAs(m4);
            AssertIndexesEqual(m3.Indexes, CID.ComponentA, CID.ComponentB);
            AssertIndexesEqual(m3.AllOfIndexes, CID.ComponentA);
            AssertIndexesEqual(m3.AnyOfIndexes, CID.ComponentB);
        }

        [Fact]
        public void UpdatesCacheWhenCallingAnyOf()
        {
            var matcher = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var cache = matcher.Indexes;
            matcher.AnyOf(CID.ComponentB);
            matcher.Indexes.Should().NotBeSameAs(cache);
        }

        [Fact]
        public void UpdatesCacheWhenCallingNoneOf()
        {
            var matcher = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var cache = matcher.Indexes;
            matcher.NoneOf(CID.ComponentB);
            matcher.Indexes.Should().NotBeSameAs(cache);
        }

        [Fact]
        public void UpdatesHashWhenChangedWithAnyOf()
        {
            var matcher = AllOfAB();
            var hash = matcher.GetHashCode();
            matcher.AnyOf(42).GetHashCode().Should().NotBe(hash);
        }

        [Fact]
        public void UpdatesHashWhenChangedWithNoneOf()
        {
            var matcher = AllOfAB();
            var hash = matcher.GetHashCode();
            matcher.NoneOf(42).GetHashCode().Should().NotBe(hash);
        }

        [Fact]
        public void EqualsEqualAllOfMatcher()
        {
            var m1 = AllOfAB();
            var m2 = AllOfAB();
            m1.Should().NotBeSameAs(m2);
            m1.Equals(m2).Should().BeTrue();
            m1.GetHashCode().Should().Be(m2.GetHashCode());
        }

        [Fact]
        public void EqualsEqualAllOfMatcherIndependentOfTheOrderOfIndexes()
        {
            var m1 = AllOfAB();
            var m2 = AllOfBA();
            m1.Equals(m2).Should().BeTrue();
            m1.GetHashCode().Should().Be(m2.GetHashCode());
        }

        [Fact]
        public void EqualsMergedMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = AllOfBA();

            var mergedMatcher = Matcher<Test1Entity>.AllOf(m1, m2);
            mergedMatcher.Equals(m3).Should().BeTrue();
            mergedMatcher.GetHashCode().Should().Be(m3.GetHashCode());
        }

        [Fact]
        public void DoesNotEqualDifferentAllOfMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = AllOfAB();
            m1.Equals(m2).Should().BeFalse();
            m1.GetHashCode().Should().NotBe(m2.GetHashCode());
        }

        [Fact]
        public void AllOfDoesNotEqualAnyOfWithSameIndexes()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AnyOf(CID.ComponentA);
            m1.Equals(m2).Should().BeFalse();
            m1.GetHashCode().Should().NotBe(m2.GetHashCode());
        }

        [Fact]
        public void DoesNotEqualDifferentTypeMatchersWithSameIndexes()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AllOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AllOf(m1, m2);
            var m4 = Matcher<Test1Entity>.AnyOf(m1, m2);
            m3.Equals(m4).Should().BeFalse();
            m3.GetHashCode().Should().NotBe(m4.GetHashCode());
        }

        [Fact]
        public void EqualsCompoundMatcher()
        {
            var m1 = Matcher<Test1Entity>.AllOf(CID.ComponentA);
            var m2 = Matcher<Test1Entity>.AnyOf(CID.ComponentB);
            var m3 = Matcher<Test1Entity>.AnyOf(CID.ComponentC);
            var m4 = Matcher<Test1Entity>.AnyOf(CID.ComponentD);

            var mX = Matcher<Test1Entity>.AllOf(m1, m2).AnyOf(m3, m4);
            var mY = Matcher<Test1Entity>.AllOf(m1, m2).AnyOf(m3, m4);

            mX.Equals(mY).Should().BeTrue();
            mX.GetHashCode().Should().Be(mY.GetHashCode());
        }

        static Test1Entity CreateEntity()
        {
            var entity = new Test1Entity();
            entity.Initialize(0, CID.TotalComponents, new Stack<IComponent>[CID.TotalComponents]);
            return entity;
        }

        static IAllOfMatcher<Test1Entity> CreateAllOfAB() => Matcher<Test1Entity>.AllOf(CID.ComponentA, CID.ComponentB);
        static IAnyOfMatcher<Test1Entity> CreateAnyOfAB() => Matcher<Test1Entity>.AnyOf(CID.ComponentA, CID.ComponentB);

        static ICompoundMatcher<Test1Entity> CreateAllOfABNoneOfCD() => Matcher<Test1Entity>
            .AllOf(CID.ComponentA, CID.ComponentB)
            .NoneOf(CID.ComponentC, CID.ComponentD);

        static ICompoundMatcher<Test1Entity> CreateAnyOfABNoneOfCD() => Matcher<Test1Entity>
            .AnyOf(CID.ComponentA, CID.ComponentB)
            .NoneOf(CID.ComponentC, CID.ComponentD);

        static ICompoundMatcher<Test1Entity> CreateAllOfABAnyOfCD() => Matcher<Test1Entity>
            .AllOf(CID.ComponentA, CID.ComponentB)
            .AnyOf(CID.ComponentC, CID.ComponentD);

        static void AssertIndexesEqual(int[] indexes, params int[] expectedIndexes)
        {
            indexes.Length.Should().Be(expectedIndexes.Length);
            for (var i = 0; i < expectedIndexes.Length; i++)
                indexes[i].Should().Be(expectedIndexes[i]);
        }

        static IAllOfMatcher<Test1Entity> AllOfAB() => Matcher<Test1Entity>.AllOf(CID.ComponentA, CID.ComponentB);
        static IAllOfMatcher<Test1Entity> AllOfBA() => Matcher<Test1Entity>.AllOf(CID.ComponentB, CID.ComponentA);
    }
}
