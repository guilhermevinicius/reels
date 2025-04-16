import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'video-upload-drag-drop',
  templateUrl: './upload-drag-drop.component.html'
})
export class UploadDraggingDropComponent {
  @Input({required: true}) title: string | null = null;
  @Output() file = new EventEmitter<File>();

  filePath: File | null = null;
  previewPath: string | null = null;

  onDragOver(event: Event): void {
    // console.log('onDragOver', event)
  }

  onDrop(event: Event): void {
    console.log('onDrop', event)
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files?.length) {
      const reader = new FileReader();
      this.filePath = input.files[0]
      reader.onload = () => {
        this.previewPath = reader.result as string;
      };
      reader.readAsDataURL(this.filePath);
    }
  }

  submit(): void {
    if (this.filePath)
      this.file.emit(this.filePath)
  }

  cleanFile() {
    this.filePath = null;
    this.previewPath = null;
  }
}
