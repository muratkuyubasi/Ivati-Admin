using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using PT.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class FrontProfile : Profile
    {
        public FrontProfile()
        {
            CreateMap<FrontAnnouncement, FrontAnnouncementDto>().ReverseMap();
            CreateMap<AddFrontAnnouncementCommand, FrontAnnouncement>();
            CreateMap<UpdateFrontAnnouncementCommand, FrontAnnouncement>();
            CreateMap<DeleteFrontAnnouncementCommand, FrontAnnouncement>();


            CreateMap<FrontAnnouncementRecord, FrontAnnouncementRecordDto>().ReverseMap();
            CreateMap<AddFrontAnnouncementRecordCommand, FrontAnnouncementRecord>();
            CreateMap<UpdateFrontAnnouncementRecordCommand, FrontAnnouncementRecord>();
            CreateMap<DeleteFrontAnnouncementRecordCommand, FrontAnnouncementRecord>();


            CreateMap<FrontGallery, FrontGalleryDto>().ReverseMap();
            CreateMap<AddFrontGalleryCommand, FrontGallery>();
            CreateMap<UpdateFrontGalleryCommand, FrontGallery>();
            CreateMap<DeleteFrontGalleryCommand, FrontGallery>();


            CreateMap<FrontGalleryMedia, FrontGalleryMediaDto>().ReverseMap();
            CreateMap<AddFrontGalleryMediaCommand, FrontGalleryMedia>();
            CreateMap<UpdateFrontGalleryMediaCommand, FrontGalleryMedia>();
            CreateMap<DeleteFrontGalleryMediaCommand, FrontGalleryMedia>();


            CreateMap<FrontGalleryRecord, FrontGalleryRecordDto>().ReverseMap();
            CreateMap<AddFrontGalleryRecordCommand, FrontGalleryRecord>();
            CreateMap<UpdateFrontGalleryRecordCommand, FrontGalleryRecord>();
            CreateMap<DeleteFrontGalleryRecordCommand, FrontGalleryRecord>();


            CreateMap<FrontMenu, FrontMenuDto>().ReverseMap();
            CreateMap<AddFrontMenuCommand, FrontMenu>();
            CreateMap<UpdateFrontMenuCommand, FrontMenu>();
            CreateMap<DeleteFrontMenuCommand, FrontMenu>();


            CreateMap<FrontMenuRecord, FrontMenuRecordDto>().ReverseMap();
            CreateMap<AddFrontMenuRecordCommand, FrontMenuRecord>();
            CreateMap<UpdateFrontMenuRecordCommand, FrontMenuRecord>();
            CreateMap<DeleteFrontMenuRecordCommand, FrontMenuRecord>();


            CreateMap<FrontPage, FrontPageDto>().ReverseMap();
            CreateMap<AddFrontPageCommand, FrontPage>();
            CreateMap<UpdateFrontPageCommand, FrontPage>();
            CreateMap<DeleteFrontPageCommand, FrontPage>();


            CreateMap<FrontPageRecord, FrontPageRecordDto>().ReverseMap();
            CreateMap<AddFrontPageRecordCommand, FrontPageRecord>();
            CreateMap<UpdateFrontPageRecordCommand, FrontPageRecord>();
            CreateMap<DeleteFrontPageRecordCommand, FrontPageRecord>();
        }
    }
}
