import {Component} from '@angular/core';
import {Button} from 'primeng/button';
import {ConfirmDialog} from 'primeng/confirmdialog';
import {Dialog} from 'primeng/dialog';
import {FloatLabel} from 'primeng/floatlabel';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {TableModule} from 'primeng/table';
import {Toast} from 'primeng/toast';
import {IGenre} from '../../models';
import {GenreService} from '../../services';
import {ConfirmationService, MessageService} from 'primeng/api';

@Component({
  selector: 'settings-genre-component',
  templateUrl: './genre.component.html',
  imports: [
    TableModule,
    Button,
    Dialog,
    ConfirmDialog,
    FloatLabel,
    FormsModule,
    ReactiveFormsModule,
    Toast
  ],
  providers: [
    GenreService,
    ConfirmationService,
    MessageService
  ]
})
export class GenreComponent {
  showDialogDelete: boolean = false
  showDialogForm: boolean = false
  genres: IGenre[] = []
  genre: IGenre | null = null

  genreForm = new FormGroup({
    name: new FormControl<string | null>(null, [Validators.required])
  })

  constructor(
    private service: GenreService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {
    this.listGenre()
  }

  getGenreAndSetForm(id: string): void {
    this.service.getGenre$(id).subscribe({
      next: data => {
        this.genre = data.data
        this.genreForm.setValue({
          name: data.data.name
        })
      },
      error: error => {
        this.messageService.add({
          summary: 'Error',
          severity: 'warning',
        })
      }
    })
  }

  listGenre() {
    this.service.listGenre$().subscribe({
      next: data => {
        this.genres = data.data
      },
      error: error => {

      }
    })
  }

  openDialogDeleteDialog($event: Event, genreId: string): void {
    this.showDialogDelete = true
    this.confirmationService.confirm({
      target: $event.target as EventTarget,
      message: 'Are you sure you want to delete this genre?',
      header: 'Confirmation',
      closable: true,
      closeOnEscape: true,
      rejectButtonProps: {
        label: 'Cancel',
        severity: 'secondary',
        outlined: true,
      },
      acceptButtonProps: {
        label: 'Save',
      },
      accept: () => {
        this.service.deleteGenre$(genreId).subscribe({
          next: data => {
            this.messageService.add({
              severity: 'success',
              summary: 'Tudo certo'
            })
            this.listGenre()
          },
          error: error => {
            this.messageService.add({
              summary: 'Algo deu errado',
              severity: 'warning',
            })
          }
        })
      },
    })
  }

  openDialogForm(id?: string): void {
    this.showDialogForm = true
    if (id)
      this.getGenreAndSetForm(id)
  }

  onSubmitForm() {
    if (this.genre) {
      this.updateGenre()
    } else {
      this.createGenre()
    }
  }

  createGenre() {
    this.service.createGenre$({
      name: this.genreForm.value.name!
    }).subscribe({
      next: data => {
        this.genreForm.reset()
        this.showDialogForm = false
        this.listGenre()
      },
      error: error => {
        this.messageService.add({
          text: 'Error ao criar genre',
        })
      }
    })
  }

  updateGenre() {
    this.service.updateGenre$({
      id: this.genre?.id!,
      name: this.genreForm.value.name!
    }).subscribe({
      next: data => {
        this.genreForm.reset()
        this.showDialogForm = false
        this.listGenre()
        this.genre = null,
          this.messageService.add({
            severity: 'success',
            summary: 'Tudo certo!',
            detail: 'Categoria'
          })
      },
      error: error => {
        this.messageService.add({
          text: 'Error ao atualizar genre',
        })
      }
    })
  }

}
