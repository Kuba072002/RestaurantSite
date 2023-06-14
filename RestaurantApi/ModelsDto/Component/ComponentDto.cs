namespace RestaurantApi.ModelsDto.Component
{
    public class ComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FreshnessTime { get; set; }
        public string Unit { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            ComponentDto other = (ComponentDto)obj;
            return this.Id == other.Id && this.Name == other.Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
