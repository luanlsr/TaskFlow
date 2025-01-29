using FluentAssertions;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.ValueObjects;
using AutoFixture;
using Xunit;
using System;
using AutoFixture.AutoNSubstitute;

namespace TaskFlow.Test.UnitTest.Domain;
public class WorkItemTests
{
    private readonly Fixture _fixture;

    public WorkItemTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoNSubstituteCustomization());
    }

    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var dueDate = _fixture.Create<DateTime>();

        // Act
        var workItem = new WorkItem(title, description, dueDate);

        // Assert
        workItem.Title.Should().Be(title);
        workItem.Description.Should().Be(description);
        workItem.DueDate.Should().Be(dueDate);
        workItem.Status.Should().Be(WorkItemStatus.Pending);
        workItem.Id.Should().NotBe(Guid.Empty);
        workItem.CreatedAt.Date.Should().Be(DateTime.UtcNow.Date);
    }

    [Fact]
    public void MarkAsCompletedAsync_ShouldSetStatusToCompleted()
    {
        // Arrange
        var workItem = _fixture.Create<WorkItem>();

        // Act
        workItem.MarkAsCompletedAsync();

        // Assert
        workItem.Status.Should().Be(WorkItemStatus.Completed);
        workItem.UpdatedAt.Date.Should().Be(DateTime.UtcNow.Date);

    }

    [Fact]
    public void MarkAsCompletedAsync_ShouldNotChangeStatus_WhenAlreadyCompleted()
    {
        // Arrange
        var workItem = _fixture.Create<WorkItem>();
        workItem.MarkAsCompletedAsync();
        var previousUpdatedAt = workItem.UpdatedAt;

        // Act
        workItem.MarkAsCompletedAsync();

        // Assert
        workItem.Status.Should().Be(WorkItemStatus.Completed);
        workItem.UpdatedAt.Should().Be(previousUpdatedAt);
    }

    [Fact]
    public void UpdateWorkItem_ShouldUpdateProperties()
    {
        // Arrange
        var workItem = _fixture.Create<WorkItem>();
        var newTitle = _fixture.Create<string>();
        var newDescription = _fixture.Create<string>();
        var newDueDate = _fixture.Create<DateTime>();

        // Act
        workItem.UpdateWorkItem(newTitle, newDescription, newDueDate);

        // Assert
        workItem.Title.Should().Be(newTitle);
        workItem.Description.Should().Be(newDescription);
        workItem.DueDate.Should().Be(newDueDate);
        workItem.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenTitleIsNullOrEmpty()
    {
        // Arrange
        string title = null;
        var description = _fixture.Create<string>();
        var dueDate = _fixture.Create<DateTime>();

        // Act
        Action act = () => new WorkItem(title, description, dueDate);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithMessage("Value cannot be null. (Parameter 'title')");
    }

    [Fact]
    public void Properties_ShouldBeImmutable()
    {
        // Arrange
        var workItem = _fixture.Create<WorkItem>();
        var originalId = workItem.Id;
        var originalCreatedAt = workItem.CreatedAt;

        // Act & Assert
        workItem.Id.Should().Be(originalId);
        workItem.CreatedAt.Should().Be(originalCreatedAt);
    }
}