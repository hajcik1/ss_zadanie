<script>
import axios from "axios";
import { useVuelidate } from "@vuelidate/core";
import { required, maxLength, between } from "@vuelidate/validators";

export default {
  setup() {
    return {
      v$: useVuelidate(),
    };
  },
  data() {
    return {
      movies: [],
      showModal: false,
      addMode: true,
      modalHeader: "",
      title: "",
      year: "",
    };
  },
  validations() {
    return {
      title: { required, maxLength: maxLength(200) },
      year: { between: between(1900, 2200) },
    };
  },
  methods: {
    refreshData() {
      axios
        .get("http://localhost:5149/api/Filmy")
        .then((response) => {
          this.movies = response.data;
        })
        .catch((error) => console.log(error));
    },
    getMovies() {
      axios
        .get("/MyMovies")
        .then((response) => {
          for (let i = 0; i < 5; i++) {
            delete response.data[i]["id"];
            axios.post("http://localhost:5149/api/Filmy", response.data[i]);
            this.refreshData();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    async addMovie() {
      const isFormCorrect = await this.v$.$validate();
      if (isFormCorrect) {
        axios
          .post("http://localhost:5149/api/Filmy", {
            title: this.title,
            director: this.director,
            year: this.year === "" ? null : this.year,
            rate: this.rate === "" ? null : this.rate,
          })
          .then((response) => {
            this.v$.$reset();
            this.refreshData();
            this.showModal = false;
            console.log(`Dodano nowy film: ${response}`);
          })
          .catch((error) => {
            console.log(error);
            alert("Film istnieje już w bazie");
          });
      }
    },
    async updateMovie() {
      const isFormCorrect = await this.v$.$validate();
      if (isFormCorrect) {
        axios
          .put("http://localhost:5149/api/Filmy/", {
            id: this.id,
            title: this.title,
            director: this.director,
            year: this.year,
            rate: this.rate,
          })
          .then((response) => {
            this.v$.$reset();
            this.refreshData();
            this.showModal = false;
            console.log(`Dodano nowy film: ${response}`);
          })
          .catch((error) => {
            console.log(error);
            alert("Wystąpił problem z API, spróbuj ponownie.");
          });
      }
    },
    deleteMovie(id) {
      if (!confirm("Film zostanie usnięty z bazy")) {
        return;
      }
      axios.delete(`http://localhost:5149/api/Filmy/${id}`).then((response) => {
        this.refreshData();
        console.log(response.data);
      });
    },
    addModal() {
      this.modalHeader = "Dodaj nowy film";
      this.addMode = true;
      this.showModal = true;
      this.id = "";
      this.title = "";
      this.director = "";
      this.year = "";
      this.rate = "";
    },
    editModal(movie) {
      this.modalHeader = "Edytuj film";
      this.addMode = false;
      this.showModal = true;
      this.id = movie.id;
      this.title = movie.title;
      this.director = movie.director;
      this.year = movie.year;
      this.rate = movie.rate;
    },
    cancelModal() {
      this.showModal = false;
      this.v$.$reset();
    },
  },
  mounted: function () {
    this.refreshData();
  },
};
</script>
<template>
  <div class="flex flex-wrap justify-center">
    <button
      @click="addModal()"
      class="py-2 px-4 mx-10 rounded-md bg-orange-500 text-slate-100 font-medium"
    >
      Dodaj film
    </button>
    <button
      @click="getMovies()"
      class="py-2 px-4 mx-10 rounded-md border-2 border-orange-500 text-orange-500 font-medium"
    >
      Pobierz filmy
    </button>
  </div>
  <div class="my-20 flex justify-center">
    <h1 v-if="movies.length < 1" class="text-xl font-bold text-slate-700">
      Brak filmów w bazie
    </h1>
    <table v-else class="w-3/4 table-fixed">
      <thead class="bg-gray-300 text-slate-700 text-left">
        <tr>
          <th class="rounded-l-md p-2">Id</th>
          <th>Tytuł</th>
          <th>Reżyser</th>
          <th>Rok</th>
          <th>Ocena</th>
          <th class="rounded-r-md"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="movie in movies">
          <td>{{ movie.id }}</td>
          <td>{{ movie.title }}</td>
          <td>{{ movie.director }}</td>
          <td>{{ movie.year }}</td>
          <td>{{ movie.rate }}</td>
          <td>
            <button
              @click="editModal(movie)"
              class="p-1 mx-5 rounded-md bg-amber-500 text-slate-100"
            >
              Edytuj
            </button>
            <button
              @click="deleteMovie(movie.id)"
              class="p-1 mx-5 rounded-md bg-red-600 text-slate-100"
            >
              Usuń
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>

  <Transition name="modal">
    <div v-if="showModal" class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header">
            <slot name="header">{{ modalHeader }}</slot>
          </div>

          <div class="modal-body">
            <form @submit.prevent="handleSubmit" class="flex flex-col">
              <input v-model="id" class="border-2 hidden" />
              <label>Tytuł filmu</label>
              <input v-model="v$.title.$model" class="border-2" />
              <div
                class="text-red-600 text-sm"
                v-if="v$.title.required.$invalid"
              >
                * Pole tytuł jest wymagane.
              </div>
              <div
                class="text-red-600 text-sm"
                v-if="v$.title.maxLength.$invalid"
              >
                * Maksymalnie 200 znaków.
              </div>
              <label>Reżyser</label>
              <input v-model="director" class="border-2" />

              <label>Rok produkcji</label>
              <input v-model="v$.year.$model" class="border-2" />
              <div class="text-red-600 text-sm" v-if="v$.year.$error">
                * Tylko przedział 1900-2200.
              </div>
              <label>Ocena</label>
              <input v-model="rate" class="border-2" />
              <div class="flex flex-wrap justify-around">
                <button
                  v-if="addMode"
                  class="modal-default-button"
                  @click="addMovie()"
                >
                  Zapisz film
                </button>
                <button
                  v-else
                  class="modal-default-button"
                  @click="updateMovie()"
                >
                  Zaktualizuj film
                </button>
                <button @click="cancelModal()">Anuluj</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </Transition>
</template>
<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}
.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
.modal-container {
  width: 500px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
}
.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}
.modal-body {
  margin: 20px 0;
}
.modal-default-button {
  float: right;
}
/*
 * The following styles are auto-applied to elements with
 * transition="modal" when their visibility is toggled
 * by Vue.js.
 *
 * You can easily play with the modal transition by editing
 * these styles.
 */
.modal-enter-from {
  opacity: 0;
}
.modal-leave-to {
  opacity: 0;
}
.modal-enter-from .modal-container,
.modal-leave-to .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}
</style>
