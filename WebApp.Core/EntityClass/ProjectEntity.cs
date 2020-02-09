using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Core.Dtos;
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

        internal List<Project> GetAll()
        {
            var projectList = DbContext.Projects.ToList();
            return projectList;
        }

        internal Project GetOne(int id)
        {
            var project = DbContext.Projects.Single(x => x.Id == id);
            return project;
        }

        internal Project PatchUpdate(int id, ProjestPatchRequest projestPatchRequest)
        {
            var project = DbContext.Projects.Single(x => x.Id == id);
            project.Completed = projestPatchRequest.Completed;
            project.UpdatedAt = DateTime.Now;
            DbContext.Update(project);
            DbContext.SaveChanges();
            return project;
        }

        internal Project PutUpdate(int id, ProjestRequest projestRequest)
        {
            var project = DbContext.Projects.Single(x => x.Id == id);
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
            var project = DbContext.Projects.Single(x => x.Id == id);
            DbContext.Remove(project);
            int response = DbContext.SaveChanges();
            return response;
        }

    }
}
