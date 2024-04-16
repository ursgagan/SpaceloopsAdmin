using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly SpaceLoopsDbContext _spaceloopsDbContext;
        public ImageRepository(SpaceLoopsDbContext spaceLoopsDbContext)
        {
            _spaceloopsDbContext = spaceLoopsDbContext;
        }

        public async Task<Image> Create(Image image)
        {
            try
            {
                if (image != null)
                {
                    var addImage = _spaceloopsDbContext.Add<Image>(image);
                    await _spaceloopsDbContext.SaveChangesAsync();
                    return addImage.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Image> GetAll()
        {
            try
            {
                var getImages = _spaceloopsDbContext.Image.Where(x => x.IsDeleted == false).ToList();
                if (getImages != null) return getImages;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Image GetById(Guid imageId)
        {
            try
            {
                if (imageId != null)
                {
                    var image = _spaceloopsDbContext.Image.FirstOrDefault(x => x.Id == imageId);
                    if (image != null) return image;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(Image image)
        {
            try
            {
                if (image != null)
                {
                    var obj = _spaceloopsDbContext.Update(image);
                    if (obj != null) _spaceloopsDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(Image image)
        {
            try
            {
                if (image != null)
                {
                    image.IsDeleted = true;
                    var obj = _spaceloopsDbContext.Update(image);
                    _spaceloopsDbContext.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

