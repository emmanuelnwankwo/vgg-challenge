﻿using System;
using System.Collections.Generic;
using WebApp.Core.BusinessLayer.Interface;
using WebApp.Core.Dtos;
using WebApp.Core.EntityClass;
using WebApp.Core.Utility;
using WebApp.Data.Models.Entities;

namespace WebApp.Core.BusinessLayer.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectEntity projectEntity;
        private ProjectResponseData projectResponse;
        public ProjectRepository(ProjectEntity _projectEntity)
        {
            projectEntity = _projectEntity;
        }

        public ProjectResponseData CreateProject(ProjestRequest projestRequest)
        {
            try
            {
                int result = projectEntity.Create(projestRequest);
                if (result != 0)
                {
                    projectResponse = new ProjectResponseData
                    {
                        Id = result,
                        Name = projestRequest.Name,
                        Description = projestRequest.Description,
                        Completed = projestRequest.Completed,
                        CreatedAt = DateTime.Now
                    };
                    return projectResponse;
                }
                return projectResponse = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectResponseData GetOneProject(int id)
        {
            try
            {
                var data = projectEntity.GetOne(id);
                projectResponse = new ProjectResponseData
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    Completed = data.Completed,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return projectResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteProject(int id)
        {
            try
            {
                int data = projectEntity.DeleteOne(id);
                if (data != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectResponseData UpdateCompleted(int id, ProjestPatchRequest projestPatchRequest)
        {
            try
            {
                var data = projectEntity.PatchUpdate(id, projestPatchRequest);
                projectResponse = new ProjectResponseData
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    Completed = data.Completed,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return projectResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectResponseData UpdateAll(int id, ProjestRequest projestRequest)
        {
            try
            {
                var data = projectEntity.PutUpdate(id, projestRequest);
                projectResponse = new ProjectResponseData
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    Completed = data.Completed,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return projectResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public PagedList<Project> GetAllProjects(ProjectResourceParameters projectResourceParameters)
        {
            try
            {
                return projectEntity.GetAll(projectResourceParameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
