

using PercobaanApi1.Entities;
using PercobaanApi1.Repositories;

namespace PercobaanApi1.Service
{
    public class CountryService
    {
        private CountryRepository countryRepository;
        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public List<Country> findAll()
        {
            return countryRepository.findAll();
        }
        public Country findById(int id)
        {
            Country country = countryRepository.findById(id);
            return country != null ? country : null;
        }
        public Country create(Country entity)
        {
            Country country = new Country();
            country.name = entity.name;
            return countryRepository.create(country);
        }
        public Country update(int id, Country entity)
        {
            Country country = new Country();
            country.region_id = id;
            country.name = entity.name;
            return countryRepository.update(country);
        }
        public Country delete(int id)
        {
            Country country = new Country();
            country.id = id;
            return countryRepository.delete(country);
        }
    }
}