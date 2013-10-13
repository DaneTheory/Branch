class H4ServiceRecord < ActiveRecord::Base
	attr_accessible :gamertag_id

	belongs_to :gamertag

	def self.insert_new_gamertag(gamertag)
		gt = Gamertag.find_by_gamertag(gamertag)
		if gt == nil
			gt = Gamertag.new(gamertag: gamertag)
			gt.save
		end

		h4_sr = new(gamertag_id: gt.id)
		h4_sr.save
	end

	def self.find_by_gamertag(gamertag)
		gamertag = Gamertag.find_by_gamertag(gamertag)
		return nil if !gamertag
		return find_by_gamertag_id(gamertag.id)
	end
end