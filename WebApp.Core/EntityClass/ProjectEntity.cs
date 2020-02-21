using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Core.Dtos;
using WebApp.Core.Utility;
using WebApp.Data;
using WebApp.Data.Models.Entities;

namespace WebApp.Core.EntityClass
{
    public class ProjectEntity
    {
        private readonly ApiDbContext DbContext;
        public ProjectEntity(ApiDbContext dbContext)
        {
            DbContext = dbContext;
        }
        internal int Create(ProjestRequest projestRequest)
        {
            try
            {
                Project newProject = new Project
                {
                    Name = projestRequest.Name,
                    Description = projestRequest.Description,
                    Completed = projestRequest.Completed,
                    CreatedAt = DateTime.Now
                };
                DbContext.Projects.Add(newProject);
                DbContext.SaveChanges();
                return newProject.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal Project GetOne(int id)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }
            return project;
        }

        internal Project PatchUpdate(int id, ProjestPatchRequest projestPatchRequest)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }
            project.Completed = projestPatchRequest.Completed;
            project.UpdatedAt = DateTime.Now;
            DbContext.Update(project);
            DbContext.SaveChanges();
            return project;
        }

        internal Project PutUpdate(int id, ProjestRequest projestRequest)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }
            project.Completed = projestRequest.Completed;
            project.Name = projestRequest.Name;
            project.Description = projestRequest.Description;
            project.UpdatedAt = DateTime.Now;
            DbContext.Update(project);
            DbContext.SaveChanges();
            return project;
        }
        internal int DeleteOne(int id)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }
            DbContext.Remove(project);
            int response = DbContext.SaveChanges();
            return response;
        }

        internal PagedList<Project> GetAll(ProjectResourceParameters projectResourceParameters)
        {
            var collectionBeforePaging = DbContext.Projects
                    .OrderBy(x => x.Name).AsQueryable();
            if (!string.IsNullOrEmpty(projectResourceParameters.Search))
            {
                var searchQuery = projectResourceParameters.Search.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Name.Contains(searchQuery)
                    || x.Description.Contains(searchQuery));
            }
            return PagedList<Project>.Create(collectionBeforePaging,
                projectResourceParameters.Offset, projectResourceParameters.Limit);

        }
    }
}
