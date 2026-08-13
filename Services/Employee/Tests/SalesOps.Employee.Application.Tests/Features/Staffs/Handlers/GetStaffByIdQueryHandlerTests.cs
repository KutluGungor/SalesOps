using Moq;
using SalesOps.Employee.Application.Features.Staffs.Handlers;
using SalesOps.Employee.Application.Features.Staffs.Queries;
using SalesOps.Employee.Application.Interfaces;
using SalesOps.Employee.Domain.Entities;

namespace SalesOps.Employee.Application.Tests.Features.Staffs.Handlers;

public class GetStaffByIdQueryHandlerTests
{
    private readonly Mock<IStaffRepository> _mockRepository;
    private readonly GetStaffByIdQueryHandler _handler;

    public GetStaffByIdQueryHandlerTests()
    {
        _mockRepository = new Mock<IStaffRepository>();
        _handler = new GetStaffByIdQueryHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenStaffExists_ReturnsStaffResult()
    {
        
        var staffId = 1;
        var staff = new Staff
        {
            Id = staffId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
            CompanyId = 5,
            BranchId = 10,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _mockRepository
            .Setup(repo => repo.GetStaffByIdAsync(staffId))
            .ReturnsAsync(staff);

        var query = new GetStaffByIdQuery(staffId);

        
        var result = await _handler.Handle(query, CancellationToken.None);

        
        Assert.NotNull(result);
        Assert.Equal(staffId, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal("john.doe@example.com", result.Email);
        Assert.Equal("1234567890", result.Phone);
        Assert.Equal(5, result.CompanyId);
        Assert.Equal(10, result.BranchId);
    }

    [Fact]
    public async Task Handle_WhenStaffNotFound_ThrowsException()
    {
        
        var staffId = 999;
        _mockRepository.Setup(repo => repo.GetStaffByIdAsync(staffId)).ReturnsAsync((Staff)null);

        var query = new GetStaffByIdQuery(staffId);

        
        var exception = await Assert.ThrowsAsync<Exception>(
            async () => await _handler.Handle(query, CancellationToken.None)
        );
        Assert.Equal("Staff not found", exception.Message);
    }

    [Fact]
    public async Task Handle_CallsRepositoryOnce()
    {
        // Arrange
        var staffId = 1;
        var staff = new Staff
        {
            Id = staffId,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane@example.com",
            Phone = "9876543210",
            CompanyId = 3,
            BranchId = 7
        };

        _mockRepository
            .Setup(repo => repo.GetStaffByIdAsync(staffId))
            .ReturnsAsync(staff);

        var query = new GetStaffByIdQuery(staffId);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockRepository.Verify(repo => repo.GetStaffByIdAsync(staffId), Times.Once);
    }
}
