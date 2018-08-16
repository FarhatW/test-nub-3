import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {IDTONode} from '../models/files/DTONode';
import {environment} from '../../../../environments/environment';

@Injectable()
export class FilesService {
  headers: Headers;
  private options = {
    headers: new HttpHeaders(
      {'Content-Type': 'application/json', responseType: 'text' as 'text'})
  };
  url = environment.apiUrl;
  private readonly filesEndPoint = this.url + 'files/';
  public readonly uploadsEndPoint = this.filesEndPoint + 'upload';
  private readonly foldersEndPoint = this.url + 'folders/';
  private readonly imagesEndPoint = 'http://images.joueclubentreprise.fr/images/produits/500x500/';
  private readonly antiCorsUrl = 'https://cors-anywhere.herokuapp.com/';

  constructor(private http: HttpClient) {
  }

  getFiles(): Observable<IDTONode> {
    return this.http.get<IDTONode>(this.filesEndPoint + 'GetFiles/')
  }

  getFolders(): Observable<IDTONode> {
    return this.http.get<IDTONode>(this.foldersEndPoint + 'GetSystemFolders/');
  }

  createFolder(paramFolder: string): Observable<void> {
    return this.http.post<void>(this.foldersEndPoint, JSON.stringify(paramFolder), this.options);
  }

  deleteFilesAndFolders(paramNode: IDTONode): Observable<void> {
    return this.http.post<void>(this.filesEndPoint,
      JSON.stringify(paramNode), this.options);
  }

  getImagePNG(refPintel: string): Observable<Blob> {
    const url = this.imagesEndPoint + refPintel + '.png';
    return this.http.get<Blob>(
      this.antiCorsUrl + url, {responseType: 'blob' as 'json'});
  }

  getImageJPG(refPintel: string): Observable<Blob> {
    const url = this.imagesEndPoint + refPintel + '.jpg';
    return this.http.get<Blob>(
      this.antiCorsUrl + url, {responseType: 'blob' as 'json'});
  }

  getImagGIF(refPintel: string): Observable<Blob> {
    const url = this.imagesEndPoint + refPintel + '.gif';
    return this.http.get<Blob>(
      this.antiCorsUrl + url, {responseType: 'blob' as 'json'});
  }
}
