using System.Collections.Generic;
using System.Linq;

namespace Entitas.Migration
{
    public class M0410_3 : IMigration
    {
        public string Version => "0.41.0-3";
        public string WorkingDirectory => "where custom TypeDrawers are located";
        public string Description => "Updating namespaces";

        public MigrationFile[] Migrate(string path)
        {
            var files = MigrationUtils.GetFiles(path).ToArray();
            var migratedFiles = new List<MigrationFile>();
            migratedFiles.AddRange(UpdateNamespace(files, "Entitas.Unity.VisualDebugging", "Entitas.VisualDebugging.Unity.Editor"));
            return migratedFiles.ToArray();
        }

        MigrationFile[] UpdateNamespace(MigrationFile[] files, string oldNamespace, string newNamespace)
        {
            var filesToMigrate = files.Where(f => f.FileContent.Contains(oldNamespace)).ToArray();
            foreach (var file in filesToMigrate)
                file.FileContent = file.FileContent.Replace(oldNamespace, newNamespace);

            return filesToMigrate;
        }
    }
}
