namespace EduHome.Business.DTOs.CourseDTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public string? CatagoryId { get; set; }

    }
}
