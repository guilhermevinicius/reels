import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Environment} from '../../../core/settings';
import {Observable} from 'rxjs';
import {IApiResponse} from '../../../core/models';
import {IGenre} from '../models';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  #httpClient = inject(HttpClient);
  #environment = inject(Environment);

  apiRouteV1 = `${this.#environment.api}/v1/genres`

  getGenre$(id: string): Observable<IApiResponse<IGenre>> {
    return this.#httpClient.get<IApiResponse<IGenre>>(`${this.apiRouteV1}/${id}`)
  }

  listGenre$(): Observable<IApiResponse<IGenre[]>> {
    return this.#httpClient.get<IApiResponse<IGenre[]>>(`${this.apiRouteV1}`)
  }

  createGenre$(body: {name: string}): Observable<IApiResponse<boolean>> {
    return this.#httpClient.post<IApiResponse<boolean>>(`${this.apiRouteV1}`, body)
  }

  updateGenre$(body: {id: string, name: string}): Observable<IApiResponse<boolean>> {
    return this.#httpClient.put<IApiResponse<boolean>>(`${this.apiRouteV1}/${body.id}`, body)
  }

  deleteGenre$(id: string): Observable<IApiResponse<boolean>> {
    return this.#httpClient.delete<IApiResponse<boolean>>(`${this.apiRouteV1}/${id}`)
  }

}
