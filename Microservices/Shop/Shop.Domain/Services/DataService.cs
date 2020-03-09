using Shop.Domain.Abstraction.Repositories;
using Shop.Domain.Abstraction.Services;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services
{
    public class DataService : IDataService
    {
        private ICsvToModelParser _parser;
        private IDataRepository[] _repositories;

        public DataService(ICsvToModelParser parser, params IDataRepository[] repositories)
        {
            _parser = parser;
            _repositories = repositories;
        }

        public async Task<string> ProcessDataStreamAsync(StreamReader streamReader)
        {
            string[] keys = null; //columns names
            int efCounter = 0, jsonCounter = 0; //Data rows counters
            var tailingData = ""; //used when received data doesn't end with \n

            using (streamReader)
            {
                var value = tailingData + (await streamReader.ReadToEndAsync()).Replace("\r", ""); //read data from body; remove '\r', merge with tailing data


                if (string.IsNullOrEmpty(value) || string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase)) //if vaule is empty continue
                {
                    return "";
                }

                var incLastIdx = value.LastIndexOf('\n') + 1; //incremented last index of
                if (incLastIdx < value.Length)
                {
                    tailingData = value.Substring(incLastIdx);
                    value = value.Substring(0, value.Length - incLastIdx);
                }
                else
                    tailingData = "";

                if (keys == null) //if columns names haven't been set yet, do it and remove first themfrom value
                {
                    try
                    {
                        keys = value.Split('\n')[0].Split(',');
                        _parser.SetKeysPositions(keys);
                        value = value.Substring(value.IndexOf('\n') + 1);
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }

                var data = _parser.ParseBatch(value); //use ICsvToModelParser object to convert string data to  models 
                
                foreach(var repo in _repositories)
                {
                    repo.
                }
                if (_efWriter != null)
                    efCounter += await _efWriter.SaveModelDataAsync(data); //use IDatasourceWriter object to write data to sqlserver
                if (_jsonWriter != null)
                    jsonCounter += await _jsonWriter?.SaveModelDataAsync(data); //use IDatasourceWriter object to write data to json file
            }

            return string.Format("Done! Rows inserted EF = {0}, JSON = {1}", efCounter, jsonCounter);
        }

        public int SaveModelData(IEnumerable<ArticleModel> data)
        {
            return SaveModelDataAsync(data).Result; //Save data synchronously
        }

        public async Task<int> SaveModelDataAsync(IEnumerable<ArticleModel> data)
        {
            _repository.BeginTransaction(); //create transaction

            try
            {
                foreach (var item in data)
                {
                    _repository.WriteData(item);
                }

                _repository.CommitTransaction(); //commit transaction after whole data is written
            }
            catch (Exception e)
            {
                _repository.RollbackTransaction(); //in case of exception rollback transaction
                Console.WriteLine(e);
            }

            return await _repository.SaveChangesAsync(); //finalize transaction
        }
    }
}
