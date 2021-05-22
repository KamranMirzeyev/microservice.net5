namespace Service.Catalog.Settings
{
    public interface IDatabaseSetting
    {
        public string CategoryCollectionName { get; set; }
        public string CourseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}