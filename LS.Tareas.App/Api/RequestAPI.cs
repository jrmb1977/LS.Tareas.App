using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using LS.Tareas.App.Models;

namespace LS.Tareas.App.Api
{
    public class ResponseAPI : Respuesta
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Resultado { get; set; }
    }

    public class RequestAPI
    {
        public string UrlRoot => ConfigurationManager.AppSettings["UrlApi"];

        public Resultado<List<TareasView>> GetTareas()// GET    api/values
        {
            string method = "GET";
            ResponseAPI resultado = SendRequest(method, null, null);
            Respuesta respuesta = resultado.GetRespuesta();
            if (respuesta.EsOK())
            {
                HttpStatusCode statusCode = resultado.StatusCode;
                string json = resultado.Resultado;
                if (statusCode.Equals(HttpStatusCode.OK))
                {
                    Resultado<List<TareasView>> response = JsonConvert.DeserializeObject<Resultado<List<TareasView>>>(json);
                    return response;
                }
                else
                {
                    Respuesta responseJson = JsonConvert.DeserializeObject<Respuesta>(json);
                    Resultado<List<TareasView>> response = new Resultado<List<TareasView>> { Codigo = responseJson.Codigo, Mensaje = responseJson.Mensaje, Data = new List<TareasView>() };
                    return response;
                }
            }
            else
            {
                Resultado<List<TareasView>> response = new Resultado<List<TareasView>> { Codigo = respuesta.Codigo, Mensaje = respuesta.Mensaje, Data = new List<TareasView>() };
                return response;
            }
        }

        public Resultado<TareaView> GetTarea(int id)// GET    api/values/5
        {
            string method = "GET";
            ResponseAPI resultado = SendRequest(method, id, null);
            Respuesta respuesta = resultado.GetRespuesta();
            if (respuesta.EsOK())
            {
                HttpStatusCode statusCode = resultado.StatusCode;
                string json = resultado.Resultado;
                if (statusCode.Equals(HttpStatusCode.OK))
                {
                    Resultado<TareaView> response = JsonConvert.DeserializeObject<Resultado<TareaView>>(json);
                    return response;
                }
                else
                {
                    Respuesta responseJson = JsonConvert.DeserializeObject<Respuesta>(json);
                    Resultado<TareaView> response = new Resultado<TareaView> { Codigo = responseJson.Codigo, Mensaje = responseJson.Mensaje, Data = new TareaView() };
                    return response;
                }
            }
            else
            {
                Resultado<TareaView> response = new Resultado<TareaView> { Codigo = respuesta.Codigo, Mensaje = respuesta.Mensaje, Data = new TareaView() };
                return response;
            }
        }

        public Respuesta CreateTarea(TareaEditView tarea)// POST   api/values (Tarea en Body)
        {
            string method = "POST";
            ResponseAPI resultado = SendRequest(method, null, tarea);
            Respuesta respuesta = resultado.GetRespuesta();
            if (respuesta.EsOK())
            {
                HttpStatusCode statusCode = resultado.StatusCode;
                string json = resultado.Resultado;
                Respuesta responseJson = JsonConvert.DeserializeObject<Respuesta>(json);
                return responseJson;
            }
            else
            {
                return respuesta;
            }
        }

        public Respuesta UpdateTarea(TareaEditView tarea)// PUT    api/values (Tarea en Body)
        {
            string method = "PUT";
            ResponseAPI resultado = SendRequest(method, null, tarea);
            Respuesta respuesta = resultado.GetRespuesta();
            if (respuesta.EsOK())
            {
                HttpStatusCode statusCode = resultado.StatusCode;
                string json = resultado.Resultado;
                Respuesta responseJson = JsonConvert.DeserializeObject<Respuesta>(json);
                return responseJson;
            }
            else
            {
                return respuesta;
            }
        }

        public Respuesta UpdateStatusTarea(int id) // PUT    api/values/5
        {
            string method = "PUT";
            ResponseAPI resultado = SendRequest(method, id, null);
            Respuesta respuesta = resultado.GetRespuesta();
            if (respuesta.EsOK())
            {
                HttpStatusCode statusCode = resultado.StatusCode;
                string json = resultado.Resultado;
                Respuesta responseJson = JsonConvert.DeserializeObject<Respuesta>(json);
                return responseJson;
            }
            else
            {
                return respuesta;
            }
        }

        public Respuesta DeleteTarea(int id) // DELETE api/values/5
        {
            string method = "DELETE";
            ResponseAPI resultado = SendRequest(method, id, null);
            Respuesta respuesta = resultado.GetRespuesta();
            if (respuesta.EsOK())
            {
                HttpStatusCode statusCode = resultado.StatusCode;
                string json = resultado.Resultado;
                Respuesta responseJson = JsonConvert.DeserializeObject<Respuesta>(json);
                return responseJson;
            }
            else
            {
                return respuesta;
            }
        }

        private ResponseAPI SendRequest(string method, int? id, TareaEditView tarea)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            string result = "";
            Respuesta respuesta = new Respuesta();
            string[] validMethods = new string[] { "GET", "POST", "PUT", "DELETE" };
            if (!validMethods.Contains(method))
            {
                respuesta.SetError(String.Format("Método NO es válido: {0}", method));
            }
            else
            {
                StringBuilder builder = new StringBuilder(UrlRoot);
                if (id.HasValue)
                    builder.AppendFormat("/{0}", id.Value);
                string url = builder.ToString();
                WebRequest request = WebRequest.Create(url);
                request.Method = method;
                request.ContentLength = 0;
                if (tarea != null)
                {
                    string jsonBody = JsonConvert.SerializeObject(tarea);
                    request.ContentType = "application/json";
                    request.ContentLength = jsonBody.Length;
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(jsonBody);
                    }
                }

                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    status = response.StatusCode;
                    using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                    {
                        result = rdr.ReadToEnd();
                    }
                }
                catch (WebException wexc)
                {
                    if (wexc.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = wexc.Response as HttpWebResponse;
                        if (response != null)
                        {
                            status = response.StatusCode;
                            using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                            {
                                result = rdr.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    respuesta.SetError(exc.Message);
                }
            }

            ResponseAPI resultado = new ResponseAPI { Codigo = respuesta.Codigo, Mensaje = respuesta.Mensaje, Resultado = result, StatusCode = status };
            return resultado;
        }

public static Resultado<string> SendRequest(string numero)
{
    //string tokenName = "JMAPICEDULA";
    string token = "BRJa1rC3JtFJ1kr0KUTPRYfryOYMi9TDVz1Tsq4j";
    string auth = String.Format("Bearer {0}", token);
    string url = String.Format("https://webservices.ec/api/cedula/{0}", numero);
    string method = "GET";

    FileLogger<string, string> logger = new FileLogger<string, string>();
    //logger.Log(url, method);

    HttpStatusCode status = HttpStatusCode.OK;
    Respuesta respuesta = new Respuesta();
    string result = "";

    WebRequest request = WebRequest.Create(url);
    request.Method = method;
    request.ContentLength = 0;
    request.Headers.Add("Authorization", auth);

    try
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        status = response.StatusCode;
        using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
        {
            result = rdr.ReadToEnd();
        }
    }
    catch (WebException wexc)
    {
        status = HttpStatusCode.InternalServerError;
        var response = wexc.Response as HttpWebResponse;
        if (response == null)
        {
            respuesta.SetError(wexc.Message);
        }
        else
        {
            status = response.StatusCode;
            using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
            {
                result = rdr.ReadToEnd();
            }
        }
    }
    catch (Exception exc)
    {
        status = HttpStatusCode.InternalServerError;
        respuesta.SetError(exc.Message);
    }

    Resultado<string> resultado = new Resultado<string>
    {
        Codigo = respuesta.Codigo,
        Mensaje = respuesta.Mensaje,
        Data = result,
        StatusCode = status
    };

    logger.Log(result);

    return resultado;
}        
    }
}
