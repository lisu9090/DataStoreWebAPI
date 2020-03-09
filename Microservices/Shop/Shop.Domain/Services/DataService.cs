using Shop.Domain.Abstraction.Repositories;
using Shop.Domain.Abstraction.Services;
using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using Shop.Domain.Utils;
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
        private string[] _keys = null; //columns names
        private string _tailingData = ""; //used when received data doesn't end with \n


        public DataService(ICsvToModelParser parser, params IDataRepository[] repositories)
        {
            _parser = parser;
            _repositories = repositories;
        }

        public async Task<string> ProcessDataStreamAsync(StreamReader streamReader)
        {
            int counter = 0; //Data rows counters
             
            using (streamReader)
            {
                var value = _tailingData + (await streamReader.ReadToEndAsync()).Replace("\r", ""); //read data from body; remove '\r', merge with tailing data

                if (string.IsNullOrEmpty(value) || string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase)) //if vaule is empty continue
                {
                    return "Empty value.";
                }

                var incLastIdx = value.LastIndexOf('\n') + 1; //incremented last index of
                if (incLastIdx < value.Length)
                {
                    _tailingData = value.Substring(incLastIdx);
                    value = value.Substring(0, value.Length - incLastIdx);
                }
                else
                    _tailingData = "";

                if (_keys == null) //if columns names haven't been set yet, do it and remove first themfrom value
                {
                    value = CutKeysFromData(value);
                }

                var data = _parser.ParseBatch(value); //use ICsvToModelParser object to convert string data to  models 
                
                foreach(var repo in _repositories)
                {
                    counter += await SaveModelDataAsync(repo, data);
                }
            }

            return string.Format("Done! Total rows inserted = {0}", counter);
        }

        public void Reset()
        {
            _keys = null;
            _tailingData = "";
        }

    private string CutKeysFromData(string value)
        {
            try
            {
                _keys = value.Split('\n')[0].Split(',');
                _parser.SetKeysPositions(_keys);
                return value.Substring(value.IndexOf('\n') + 1);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        private async Task<int> SaveModelDataAsync(IDataRepository repository, IEnumerable<ArticleModel> data)
        {
            repository.BeginTransaction(); //create transaction

            try
            {
                foreach (var item in data)
                {
                    repository.WriteData(item);
                }

                repository.CommitTransaction(); //commit transaction after whole data is written
            }
            catch (Exception e)
            {
                repository.RollbackTransaction(); //in case of exception rollback transaction
                Console.WriteLine(e);
            }

            return await repository.SaveChangesAsync(); //finalize transaction
        }
    }
}
