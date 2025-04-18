import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Environment} from '../../../core/settings';
import {Observable} from 'rxjs';
import {IApiResponse} from '../../../core/models';
import {IVideo, IVideoRequest} from '../models';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  #httpClient = inject(HttpClient);
  #environment = inject(Environment);

  apiRouteV1 = `${this.#environment.api}/v1/videos`

  getVideo$(id: string): Observable<IApiResponse<IVideo>> {
    return this.#httpClient.get<IApiResponse<IVideo>>(`${this.apiRouteV1}/${id}`)
  }

  listVideo$(): Observable<IApiResponse<IVideo[]>> {
    return this.#httpClient.get<IApiResponse<IVideo[]>>(this.apiRouteV1)
  }

  createVideo$(body: FormData): Observable<IApiResponse<boolean>> {
    return this.#httpClient.post<IApiResponse<boolean>>(this.apiRouteV1, body);
  }

  updateVideo$(id:string, body: FormData): Observable<IApiResponse<boolean>> {
    return this.#httpClient.put<IApiResponse<boolean>>(`${this.apiRouteV1}/${id}`, body);
  }
}
