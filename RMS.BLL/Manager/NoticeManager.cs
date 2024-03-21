using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class NoticeManager : BaseManager, INoticeManager
    {
        private INoticeRepository _noticeRepository;

        public NoticeManager()
        {
            _noticeRepository = new NoticeRepository();
        }
        public List<Notice> GetNotices()
        {
            DateTime currentDate = DateTime.Now.Date;
            return _noticeRepository.Filter(x =>x.Status ==true).ToList();
        }

        public Notice FindOne(int noticeInfoIdentity)
        {
            return _noticeRepository.FindOne(x => x.NoticeIdentity == noticeInfoIdentity);
        }

        public ResponseModel Save(Notice notice)
        {
            Notice old = _noticeRepository.FindOne(x => x.NoticeIdentity == notice.NoticeIdentity);
            if (old == null)
            {
                ResponseModel.ResponsStatus = _noticeRepository.Save(notice);
                ResponseModel.Message = ResponseModel.ResponsStatus >= 0 ? "Data has been saved." : "Data is not saved.";

            }
            else
            {
                old.Titile = notice.Titile;
                old.Description = notice.Description;
                old.ExpireDate = notice.ExpireDate;
                ResponseModel.ResponsStatus = _noticeRepository.Edit(old);
                ResponseModel.Message = ResponseModel.ResponsStatus >= 0 ? "Data has been updated." : "Data is not updated.";

            }
               return ResponseModel;
        }

        public ResponseModel Delete(int noticeInfoIdentity)
        {
            Notice oldData = _noticeRepository.FindOne(x => x.NoticeIdentity == noticeInfoIdentity);
            oldData.Status = false;
            ResponseModel.ResponsStatus = _noticeRepository.Edit(oldData);
            ResponseModel.Message = ResponseModel.ResponsStatus >= 0 ? "Data has been deleted." : "Data is not deleted.";
            return ResponseModel;
        }

        public Notice FindOneLast()
        {
            DateTime currentDate = DateTime.Now.Date;
            return _noticeRepository.Filter(x => x.Status == true && x.ExpireDate >= currentDate && x.PublishDate<= currentDate).OrderByDescending(x => x.NoticeIdentity).FirstOrDefault();
        }

        public int UpdateLogInStatus(string clientMachineName, string clientIP, long userId, bool isValidate)
        {
            int isUpdated = _noticeRepository.UpdateUserLoginStatus(clientMachineName, clientIP, userId, isValidate);
            return isUpdated;
        }

        public int UpdateLogOffStatus(long userId)
        {
            return  _noticeRepository.UpdateLogOff(userId);
        }

        public int UpdateLogInStatus(string clientMachineName, string clientIP)
        {
            throw new NotImplementedException();
        }
    }
}
