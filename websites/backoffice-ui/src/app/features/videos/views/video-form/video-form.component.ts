import {Component} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterModule} from '@angular/router';
import {UploadDraggingDropComponent} from '../../components/uploder-drag-drop/upload-drag-drop.component';
import {InputComponent, SelectInputComponent, TextAreaInputComponent} from '../../../../components';
import {VideoService} from '../../services';
import {IVideoRequest} from '../../models';
import {CommonModule, JsonPipe} from '@angular/common';

@Component({
  selector: 'video-form',
  templateUrl: './video-form.component.html',
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    UploadDraggingDropComponent,
    InputComponent,
    SelectInputComponent,
    TextAreaInputComponent,
  ]
})
export class VideoFromComponent {
  file: File | null = null;

  videoForm = new FormGroup({
    title: new FormControl<string | null>(null, Validators.required),
    description: new FormControl<string | null>(null, Validators.required),
    yearLaunched: new FormControl<number | null>(null, Validators.required),
    opened: new FormControl<boolean>(false, Validators.required),
    published: new FormControl<boolean>(false, Validators.required),
    duration: new FormControl<number | null>(null, Validators.required),
    rating: new FormControl<number | null>(null, Validators.required),
    category: new FormControl<string | null>(null, Validators.required)
  })

  ratings = [
    {name: 'ER', value: 0},
    {name: 'L', value: 1},
    {name: 'Rate10', value: 2},
    {name: 'Rate12', value: 3},
    {name: 'Rate14', value: 4},
    {name: 'Rate16', value: 5},
    {name: 'Rate18', value: 6}
  ]

  categories = [
    { name: 'category', value: 'category' },
    { name: 'category', value: 'category' }
  ]

  constructor(
    private videoService: VideoService,
    private router: Router
  ) {
  }

  onSubmit(): void {
    const fields = this.videoForm.getRawValue();

    const body: IVideoRequest = {
      title: fields.title!,
      description: fields.description!,
      yearLaunched: Number(fields.yearLaunched!),
      opened: fields.opened!,
      published: fields.published!,
      duration: fields.duration!,
      rating: Number(fields.rating!)
    }

    this.videoService.createVideo$(body).subscribe({
      next: params => {
        this.router.navigateByUrl('/videos')
      },
      error: err => {
      }
    })
  }
}
