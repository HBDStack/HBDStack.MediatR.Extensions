using FluentAssertions;
using HBD.Results.Tests.Data;
using HBDStack.Results;

namespace HBD.Results.Tests;

public class ResultTests
{
    [Fact]
    public void Result_Test()
    {
        var rs = Result.Ok();
        rs.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Result_Fails_WithReason_Test()
    {
        var rs = Result.Fails("I'm failed.", new[] { "Address" });
        rs.IsSuccess.Should().BeFalse();
        rs.Errors.First().Reasons.First().Should().Be("Address");
    }

    [Fact]
    public void Result_Fails_OtherError_Test()
    {
        var rs = Result.Fails("I'm failed.", new[] { "Address" })
            .WithError("Another error here.");
        rs.IsSuccess.Should().BeFalse();
        rs.Errors.First().Reasons.First().Should().Be("Address");
    }

    [Fact]
    public void Result_Fails_Exception_Test()
    {
        void Action()
        {
            throw new ArgumentNullException(nameof(TestData.Name));
        }

        var rs = Result.OkIf(Action);

        rs.IsSuccess.Should().BeFalse();
        rs.Errors.Last().Message.Should().Be("Value cannot be null. (Parameter 'Name')");
    }

    [Fact]
    public async Task ResultAsync_Fails_Exception_Test()
    {
        Task Action()
        {
            throw new ArgumentNullException(nameof(TestData.Name));
        }

        var rs = await Result.OkIf(Action);

        rs.IsSuccess.Should().BeFalse();
        rs.Errors.Last().Message.Should().Be("Value cannot be null. (Parameter 'Name')");
    }

    [Fact]
    public void Result_Fails_WithMultiReason_Test()
    {
        var rs = Result.Fails("I'm failed.", new[] { "Address" })
            .WithError("I'm failed.", new[] { "Address" });

        rs.IsSuccess.Should().BeFalse();
        rs.Errors.First().Reasons.First().Should().Be("Address");
    }

    [Fact]
    public void Result_Failed_Test()
    {
        var rs = Result.Fails("Something went wrong!");
        rs.IsSuccess.Should().BeFalse();
        rs.Errors[0].Message.Should().Be("Something went wrong!");
    }

    [Fact]
    public void Result_WithValue_Test()
    {
        var rs = Result.Ok(new TestData());
        rs.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void ResultGenric_WithNull_Test()
    {
        var rs = () => Result.Ok<TestData>(null!);
        rs.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ResultGeneric_Failed_Test()
    {
        var rs = Result.Fails<TestData>("Something went wrong!");
        rs.IsSuccess.Should().BeFalse();
        rs.Errors[0].Message.Should().Be("Something went wrong!");
    }

    [Fact]
    public void ResultGeneric_Fails_WithReason_Test()
    {
        var rs = Result.Fails<TestData>("I'm failed.", new[] { "Address" });
        rs.IsSuccess.Should().BeFalse();
        rs.Value.Should().BeNull();
        rs.Errors.First().Reasons.First().Should().Be("Address");
    }

    [Fact]
    public void ResultGeneric_Fails_WithMultiReason_Test()
    {
        var rs = Result.Fails<TestData>("I'm failed.", new[] { "Address" })
            .WithError("I'm failed.", new[] { "Address" });

        rs.IsSuccess.Should().BeFalse();
        rs.Value.Should().BeNull();
        rs.Errors.First().Reasons.First().Should().Be("Address");
    }

    [Fact]
    public void ResultGeneric_Fails_OtherError_Test()
    {
        var rs = Result.Fails<TestData>("I'm failed.", new[] { "Address" })
            .WithError("Another error here.");

        rs.IsSuccess.Should().BeFalse();
        rs.Value.Should().BeNull();
        rs.Errors.First().Reasons.First().Should().Be("Address");
    }

    [Fact]
    public void ResultGeneric_Fails_Exception_Test()
    {
        TestData Action()
        {
            throw new ArgumentNullException(nameof(TestData.Name));
        }

        var rs = Result.OkIf(Action);

        rs.IsSuccess.Should().BeFalse();
        rs.Value.Should().BeNull();
        rs.Errors.Last().Message.Should().Be("Value cannot be null. (Parameter 'Name')");
    }

    [Fact]
    public async Task ResultGenericAsync_Fails_Exception_Test()
    {
        Task<TestData> Action()
        {
            throw new ArgumentNullException(nameof(TestData.Name));
        }

        var rs = await Result.OkIf(Action);

        rs.IsSuccess.Should().BeFalse();
        rs.Value.Should().BeNull();
        rs.Errors.Last().Message.Should().Be("Value cannot be null. (Parameter 'Name')");
    }
}