import {inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {IApiResponse} from '../../../core/models';
import {ICategory} from '../models';
import {Environment} from '../../../core/settings';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  #httpClient = inject(HttpClient);
  #environment = inject(Environment);

  apiRouteV1 = `${this.#environment.api}/v1/categories`

  getCategory$(id: string): Observable<IApiResponse<ICategory>> {
    return this.#httpClient.get<IApiResponse<ICategory>>(`${this.apiRouteV1}/${id}`)
  }

  listCategory$(): Observable<IApiResponse<ICategory[]>> {
    return this.#httpClient.get<IApiResponse<ICategory[]>>(`${this.apiRouteV1}`)
  }

  createCategory$(body: {name: string, description: string}): Observable<IApiResponse<boolean>> {
    return this.#httpClient.post<IApiResponse<boolean>>(`${this.apiRouteV1}`, body)
  }

  updateCategory$(body: {id: string, name: string, description: string}): Observable<IApiResponse<boolean>> {
    return this.#httpClient.put<IApiResponse<boolean>>(`${this.apiRouteV1}/${body.id}`, body)
  }

  deleteCategory$(id: string): Observable<IApiResponse<boolean>> {
    return this.#httpClient.delete<IApiResponse<boolean>>(`${this.apiRouteV1}/${id}`)
  }

}
