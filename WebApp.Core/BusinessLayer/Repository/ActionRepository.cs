using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.BusinessLayer.Repository;
using WebApp.Core.Dtos;
using WebApp.Core.EntityClass;

namespace WebApp.Core.BusinessLayer.Interface
{
    public class ActionRepository : IActionRepository
    {
        private ActionEntity actionEntity;
        private AuthEntity authEntity;
        private ActionResponseData responseData;
        public ActionRepository(ActionEntity _actionEntity, AuthEntity _authEntity)
        {
            actionEntity = _actionEntity;
            authEntity = _authEntity;
        }
        public ActionResponseData CreateAction(int id, ActionRequest actionRequest)
        {
            try
            {
                int result = actionEntity.Create(id, actionRequest);
                if (result != 0)
                {
                    responseData = new ActionResponseData
                    {
                        Id = result,
                        Project_Id = id,
                        Note = actionRequest.Note,
                        Description = actionRequest.Description,
                        CreatedAt = DateTime.Now
                    };
                    return responseData;
                }
                return responseData = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ActionResponseData> GetAllActions()
        {
            try
            {
                var response = actionEntity.GetAll();
                List<ActionResponseData> list = new List<ActionResponseData>();
                foreach (var data in response)
                {
                    responseData = new ActionResponseData
                    {
                        Id = data.Id,
                        Project_Id = data.Project_Id,
                        Note = data.Note,
                        Description = data.Description,
                        CreatedAt = data.CreatedAt,
                        UpdatedAt = data.UpdatedAt
                    };
                    list.Add(responseData);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResponseData GetOneAction(int id)
        {
            try
            {
                var data = actionEntity.GetOne(id);
                responseData = new ActionResponseData
                {
                    Id = data.Id,
                    Project_Id = data.Project_Id,
                    Note = data.Note,
                    Description = data.Description,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return responseData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteAction(int projectId, int actionId)
        {
            try
            {
                int data = actionEntity.DeleteOne(projectId, actionId);
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

        public List<ActionResponseData> GetAllActionsInProject(int id)
        {
            try
            {
                var response = actionEntity.GetActionsInProject(id);
                List<ActionResponseData> list = new List<ActionResponseData>();
                foreach (var data in response)
                {
                    responseData = new ActionResponseData
                    {
                        Id = data.Id,
                        Project_Id = data.Project_Id,
                        Note = data.Note,
                        Description = data.Description,
                        CreatedAt = data.CreatedAt,
                        UpdatedAt = data.UpdatedAt
                    };
                    list.Add(responseData);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResponseData GetActionInProject(int projectId, int actionId)
        {
            try
            {
                var data = actionEntity.GetActionInProject(projectId, actionId);
                responseData = new ActionResponseData
                {
                    Id = data.Id,
                    Project_Id = data.Project_Id,
                    Note = data.Note,
                    Description = data.Description,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return responseData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResponseData GetActionInProject(int projectId, int actionId, ActionRequest actionRequest)
        {
            try
            {
                var data = actionEntity.ProjectPutUpdate(projectId, actionId, actionRequest);
                responseData = new ActionResponseData
                {
                    Id = data.Id,
                    Note = data.Note,
                    Description = data.Description,
                    Project_Id = data.Project_Id,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return responseData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AccessAuthentication(string token)
        {
            try
            {
                bool isValid = authEntity.AccessAuthentication(token);
                if (isValid)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
