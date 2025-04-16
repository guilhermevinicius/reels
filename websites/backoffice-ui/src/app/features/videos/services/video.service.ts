import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Environment} from '../../../core/settings';
import {Observable} from 'rxjs';
import {IApiResponse} from '../../../core/models';
import {IVideoRequest} from '../models';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  #httpClient = inject(HttpClient);
  #environment = inject(Environment);

  apiRouteV1 = `${this.#environment.api}/v1/videos`

  createVideo$(body: IVideoRequest): Observable<IApiResponse<boolean>> {
    return this.#httpClient.post<IApiResponse<boolean>>(this.apiRouteV1, body);
  }

}
