export interface IApiResponse<T> {
  success: boolean
  data: T
  messages: Array<string>
}
