using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using static RSMNG.TAUMEDIKA.Model;

namespace RSMNG.TAUMEDIKA
{
    public class Helper
    {

        public static void ThrowTestException(bool isThrow)
        {
            if (isThrow)
            {
                // Simulate an exception
                throw new InvalidOperationException("This is a test exception to force a catch block.");
            }

        }
        public static void SetStateCode(IOrganizationService service, string entityName, Guid entityID, int stateIn, int statusIn)
        {
            try
            {
                SetStateRequest updateStatus = new SetStateRequest();
                updateStatus.EntityMoniker = new EntityReference(entityName, entityID);
                updateStatus.State = new OptionSetValue(stateIn);
                updateStatus.Status = new OptionSetValue(statusIn);
                service.Execute(updateStatus);
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }
        public static string UploadFile(ITracingService tracingService, IOrganizationService service, string fieldName, string entityName, string jsonDataInput)
        {
            Model.UploadFile_Output output = new Model.UploadFile_Output();
            string jsonDataOutput = string.Empty;
            try
            {
                Model.UploadFile_Input input = RSMNG.Plugins.Controller.Deserialize<Model.UploadFile_Input>(jsonDataInput, typeof(Model.UploadFile_Input));

                var limit = 4194304;
                var blockIds = new List<string>();
                var data = Convert.FromBase64String(input.Content);

                var initializeFileUploadRequest = new InitializeFileBlocksUploadRequest
                {
                    FileAttributeName = fieldName,
                    Target = new EntityReference(entityName, new Guid(input.Id)),
                    FileName = input.FileName
                };
                var fileUploadResponse = (InitializeFileBlocksUploadResponse)service.Execute(initializeFileUploadRequest);

                for (int i = 0; i < Math.Ceiling(input.FileSize / Convert.ToDecimal(limit)); i++)
                {
                    var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                    blockIds.Add(blockId);
                    var blockData = data.Skip(i * limit).Take(limit).ToArray();
                    var blockRequest = new UploadBlockRequest() { FileContinuationToken = fileUploadResponse.FileContinuationToken, BlockId = blockId, BlockData = blockData };
                    var blockResponse = (UploadBlockResponse)service.Execute(blockRequest);
                }

                var commitRequest = new CommitFileBlocksUploadRequest()
                {
                    BlockList = blockIds.ToArray(),
                    FileContinuationToken = fileUploadResponse.FileContinuationToken,
                    FileName = input.FileName,
                    MimeType = input.MimeType,
                };

                service.Execute(commitRequest);

            }
            catch (Exception exception)
            {
                output.result = 2;
                output.message = exception.Message;
            }
            finally
            {
                jsonDataOutput = RSMNG.Plugins.Controller.Serialize<UploadFile_Output>(output, typeof(UploadFile_Output));

            }
            return jsonDataOutput;
        }
        public static string DownloadFile(ITracingService tracingService, IOrganizationService service, string fieldName, string entityName, string jsonDataInput)
        {
            DownloadFile_Output output = new DownloadFile_Output();
            string jsonDataOutput = string.Empty;
            string data = null;

            try
            {

                List<byte> totalFile = new List<byte>();

                long blockChunkSize = 4194304;

                InitializeFileBlocksDownloadRequest initializeFile = new InitializeFileBlocksDownloadRequest
                {
                    FileAttributeName = fieldName,
                    Target = new EntityReference(entityName, new Guid(jsonDataInput))
                };
                InitializeFileBlocksDownloadResponse fileResponse = (InitializeFileBlocksDownloadResponse)service.Execute(initializeFile);
                string fileContinuationToken = fileResponse.FileContinuationToken;
                long retFileBytes = fileResponse.FileSizeInBytes;
                while (totalFile.Count < retFileBytes)
                {
                    if (retFileBytes - totalFile.Count < blockChunkSize)
                    {
                        blockChunkSize = retFileBytes - totalFile.Count;
                    }
                    DownloadBlockRequest req = new DownloadBlockRequest { Offset = totalFile.Count, FileContinuationToken = fileContinuationToken, BlockLength = blockChunkSize };
                    DownloadBlockResponse response = (DownloadBlockResponse)service.Execute(req);
                    totalFile.AddRange(response.Data);
                    fileContinuationToken = req.FileContinuationToken;
                }
                data = Convert.ToBase64String(totalFile.ToArray());

            }
            catch (Exception exception)
            {
                output.result = 2;
                output.message = exception.Message;
            }
            finally
            {
                output.Data = data;
                jsonDataOutput = RSMNG.Plugins.Controller.Serialize<DownloadFile_Output>(output, typeof(DownloadFile_Output));
            }
            return jsonDataOutput;
        }
        public static string RemoveSpecialCharacters(string input)
        {
            StringBuilder result = new StringBuilder();

            char[] caratteriAccentati = new char[] { 'à', 'è', 'é', 'ù', 'ì', 'ò', 'â', 'ê', 'î', 'ô', 'û', 'Ä', 'Ö', 'Ü', 'Ç', 'ç', 'À', 'È', 'Ì', 'Ò', 'Ù', 'É', 'Ê', 'Ô', 'Â', 'Î' };

            // Sostituisci i caratteri accentati con i loro valori Unicode
            foreach (char c in input)
            {
                // Controlla se il carattere è accentato
                if (Array.Exists(caratteriAccentati, element => element == c))
                {
                    // Aggiungi il codice Unicode in esadecimale
                    result.AppendFormat("\\u{0:X4}", (int)c);
                }
                else
                {
                    // Altrimenti, aggiungi il carattere originale
                    result.Append(c);
                }
            }
            return result.ToString();
        }
        public static string EscapeSpecialChars(string str)
        {
            // Mappa dei caratteri reali e i loro equivalenti HTML
            var charMap = new Dictionary<string, string>
        {
            { "&", "&amp;" },
            { "<", "&lt;" },
            { ">", "&gt;" },
            { "\"", "&quot;" },
            { "'", "&#39;" },
            { "/", "&#x2F;" },
            { "\\", "&#x5C;" },
            { " ", "&nbsp;" }
        };

            // Itera sui caratteri nella mappa e sostituisce quelli presenti nella stringa
            foreach (var entry in charMap)
            {
                str = str.Replace(entry.Key, entry.Value);
            }

            return str;
        }
        public static string UnescapeSpecialChars(string str)
        {
            var charMap = new Dictionary<string, string>
            {
                { "&amp;", "&" },
                { "&lt;", "<" },
                { "&gt;", ">" },
                { "&quot;", "\"" },
                { "&#39;", "'" },
                { "&#x2F;", "/" },
                { "&#x5C;", "\\" },
                { "&nbsp;", " " }
            };

            foreach (var entry in charMap)
            {
                str = str.Replace(entry.Key, entry.Value);
            }

            return str;
        }
        public static void updateEntityStatusCode(IOrganizationService service, ITracingService trace, string entityLogicalName, string entityIdString, int statecode, int statuscode)
        {
            Guid entityId = new Guid(entityIdString);
            Entity entity = new Entity(entityLogicalName, entityId);

            entity["statecode"] = new OptionSetValue(statecode);
            entity["statuscode"] = new OptionSetValue(statuscode);

            service.Update(entity);
        }
    }
    public class CustomStringWriter : StringWriter
    {
        private readonly Encoding encoding;

        public CustomStringWriter(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
    public class Model
    {
        [DataContract]
        public class BasicOutput
        {
            [DataMember] public int result { get; set; }
            [DataMember] public string message { get; set; }
        }
        [DataContract]
        public class UploadFile_Input
        {
            [DataMember] public string Content { get; set; }
            [DataMember] public int FileSize { get; set; }
            [DataMember] public string FileName { get; set; }
            [DataMember] public string MimeType { get; set; }
            [DataMember] public string Id { get; set; }
        }
        [DataContract]
        public class UploadFile_Output : BasicOutput
        {
            public UploadFile_Output()
            {
                result = 0;
                message = "Upload effettuato con successo.";
            }
        }
        [DataContract]
        public class DownloadFile_Output : BasicOutput
        {
            [DataMember] public string Data { get; set; }
            public DownloadFile_Output()
            {
                result = 0;
                message = "Download effettuato con successo.";
            }
        }
    }
    public class Response
    {
        public string Code { get; }
        public string Message { get; }
        /// <summary>
        /// entity response expando for Custom API output parameter 'error'
        /// </summary>
        public Entity ErrorResponseEntity { get; }
        public Response(string code, string message)
        {
            Code = code;
            Message = message;
            ErrorResponseEntity = new Entity();
            ErrorResponseEntity.Attributes.Add("code", Code);
            ErrorResponseEntity.Attributes.Add("message", Message);
        }

        public Response(ref IPluginExecutionContext context, string result, string code, string message) : this(code, message)
        {
            context.OutputParameters["result"] = result;
            context.OutputParameters["error"] = ErrorResponseEntity;
        }
    }
}
