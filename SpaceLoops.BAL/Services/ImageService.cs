
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = SpaceLoops.DAL.Entities.Image;

namespace SpaceLoops.BAL.Services
{
    public class ImageServices
    {
        private readonly IRepository<Image> _imageRepository;
        public ImageServices(IRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Image> AddImage(Image image)
        {
            try
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }
                else
                {
                    image.IsDeleted = false;
                    image.CreatedOn = DateTime.Now;
                    image.UpdatedOn = DateTime.Now;
                    return await _imageRepository.Create(image);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Image> GetAllImages()
        {
            try
            {
                return _imageRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Image GetImageById(Guid Id)
        {
            try
            {
                return _imageRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Image UpdateImage(Image image)
        {
            try
            {
                if (image.Id != null || image.Id != Guid.Empty)
                {
                    var obj = _imageRepository.GetAll().Where(x => x.Id == image.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.ImageName = image.ImageName;
                        obj.ImageData = image.ImageData;
                        obj.UpdatedOn = DateTime.Now;
                        _imageRepository.Update(obj);
                    }
                }
                return image;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteImage(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var image = _imageRepository.GetById(id);
                    _imageRepository.Delete(image);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

