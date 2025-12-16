using Backend.Domain.Lessons;
using FluentAssertions;

namespace Backend.Domain.Tests.Unit.Lessons;

public class LessonTests() {
    [Fact]
    public void Create_ShouldReturnError_WhenTitleIsEmpty() {
        // Arrange
        const string someInvalidTitle = "";

        // Act
        var result = Lesson.Create(someInvalidTitle, 1);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(LessonErrors.TitleEmpty);
    }

    [Fact]
    public void Create_ShouldReturnLesson_WhenInputsAreValid() {
        // Arrange
        const string someValidTitle = "What is React?";

        // Act
        var result = Lesson.Create(someValidTitle, 1);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Title.Should().Be(someValidTitle);
    }
}
