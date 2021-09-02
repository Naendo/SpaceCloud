<template>
  <div class="DbNewsfeed">
    <TitleH3 text="What's new?" />
    <div class="news-container scrollable">
      <div v-for="(newsitem, id) in news" :key="id" class="news-item">
        <p class="news-title">
          {{ newsitem.title }}
          <span v-if="newsitem.new" class="news-title-new">New!</span>
        </p>
        <p class="news-text">{{ newsitem.text }}</p>
      </div>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
// import api from '@/services/api';

export default {
  name: 'DbNewsfeed',
  components: { TitleH3 },
  props: { tickets: Array },
  data() {
    return {
      news: [
        {
          title: 'Meeting Room 04 reopened!',
          text:
            'After being under construction for a few weeks, the Meeting Room 04 is now open again and ready to be used!',
          new: true,
        },
        {
          title: 'New Parking Spaces',
          text:
            'We’re happy to announce, that there are now ten new parking spots, which can be booked manually!',
          new: true,
        },
        {
          title: 'New Meeting Room on First Floor',
          text:
            'There is now a new Meeting Room on the First Floor. Its’ special Features are a whiteboard and a beamer. The capacity is ten people.',
          new: false,
        },
        {
          title: 'Vending machine fixed',
          text:
            'Our Vending machine in the kitchen/cafeteria has been fixed and is ready to be used again.',
          new: false,
        },
      ],
    };
  },
};
</script>
<style scoped lang="scss">
.DbNewsfeed {
  padding: 25px;
  background-color: $tile-background;
  border-radius: 20px;
}

.news-container {
  height: 93%;
}

.news-item {
  // height: 100px;
  width: 100%;

  border-bottom: 1px solid #dbdbdb;

  margin: 10px 0;
  padding: 25px 0;
}

.news-title {
  font-weight: bold;
  margin-bottom: 0.8em;
  font-size: 1.2em;
}

.news-title-new {
  color: $red;
  font-weight: bold;
  margin-left: 8px;
  font-size: 0.8em;
}
</style>
